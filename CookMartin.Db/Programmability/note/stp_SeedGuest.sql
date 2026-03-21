CREATE PROCEDURE [note].[stp_SeedGuest]
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM [note].[Collections] WHERE [Name] != 'Default Guest Collection' AND [IsDeleted] = 0)
	BEGIN
		INSERT INTO [note].[Collections] ([UserId], [Name], [CreatedDate], [IsDeleted])
		VALUES ('guest', 'Default Guest Collection', GETUTCDATE(), 0);
	END

	DECLARE @TestCardCount INT;
	DECLARE @TestCollectionId INT;

	SELECT @TestCollectionId = [CollectionId] 
	FROM [note].[Collections] 
	WHERE [Name] = 'Default Guest Collection' AND [UserId] = 'guest';

	SELECT @TestCardCount = COUNT(*)
	FROM [note].[Notecards]
	WHERE [CollectionId] = @TestCollectionId;

	DECLARE @InsertedCount INT = 0;

	IF @TestCardCount < 20
	BEGIN
		DECLARE @i INT = 1;
			WHILE @i <= 20
			BEGIN
				INSERT INTO [note].[Notecards] ([CollectionId], [FrontDescription], [BackDescription], [CreatedDate], [IsDeleted])
				VALUES (
					@TestCollectionId, 
					CONCAT('Test Front ', @i, '-', CONVERT(VARCHAR, CURRENT_TIMESTAMP, 120)), 
					CONCAT('Test Back ', @i, '-', CONVERT(VARCHAR, CURRENT_TIMESTAMP, 120)), 
					GETUTCDATE(), 
					0
				);
				SET @i = @i + 1;
				SET @InsertedCount = @InsertedCount + 1;
			END
	END

	SELECT @InsertedCount AS CardsInserted, @TestCollectionId AS CollectionId;
END

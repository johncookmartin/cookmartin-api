CREATE PROCEDURE [note].[stp_CreateCollection]
    @UserId NVARCHAR(450),
    @Name NVARCHAR(255),
    @CollectionId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [note].[Collections] ([UserId], [Name], [CreatedDate], [IsDeleted])
    VALUES (@UserId, @Name, GETUTCDATE(), 0);

    SET @CollectionId = SCOPE_IDENTITY();

    SELECT @CollectionId AS CollectionId;
END

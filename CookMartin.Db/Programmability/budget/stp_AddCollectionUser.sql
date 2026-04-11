CREATE PROCEDURE [budget].[stp_AddCollectionUser]
    @CollectionId     INT,
    @UserId           NVARCHAR(450),
    @CollectionUserId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [budget].[CollectionUsers] ([CollectionId], [UserId], [CreatedDate], [IsDeleted])
    VALUES (@CollectionId, @UserId, GETUTCDATE(), 0);
    SET @CollectionUserId = SCOPE_IDENTITY();
    SELECT @CollectionUserId AS CollectionUserId;
END

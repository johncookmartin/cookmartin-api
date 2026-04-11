CREATE PROCEDURE [budget].[stp_RemoveCollectionUser]
    @CollectionId INT,
    @UserId       NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [budget].[CollectionUsers]
    SET [IsDeleted] = 1
    WHERE [CollectionId] = @CollectionId
        AND [UserId] = @UserId
        AND [IsDeleted] = 0;
    SELECT @@ROWCOUNT AS RowsAffected;
END

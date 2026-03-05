CREATE PROCEDURE [note].[stp_GetCollectionsByUser]
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [CollectionId],
        [Name],
        [UserId],
        [CreatedDate],
        [UpdatedDate],
        [IsDeleted]
    FROM [note].[Collections]
    WHERE [UserId] = @UserId 
        AND [IsDeleted] = 0
    ORDER BY [CreatedDate] DESC;
END

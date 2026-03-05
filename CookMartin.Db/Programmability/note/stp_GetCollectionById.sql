CREATE PROCEDURE [note].[stp_GetCollectionById]
    @CollectionId INT
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
    WHERE [CollectionId] = @CollectionId
        AND [IsDeleted] = 0;
END

CREATE PROCEDURE [note].[stp_GetCollectionWithNotecardCount]
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.[CollectionId],
        c.[Name],
        c.[UserId],
        c.[CreatedDate],
        c.[UpdatedDate],
        c.[IsDeleted],
        COUNT(n.[NotecardId]) AS NotecardCount
    FROM [note].[Collections] c
    LEFT JOIN [note].[Notecards] n ON c.[CollectionId] = n.[CollectionId] 
        AND n.[IsDeleted] = 0
    WHERE c.[UserId] = @UserId
        AND c.[IsDeleted] = 0
    GROUP BY 
        c.[CollectionId],
        c.[Name],
        c.[UserId],
        c.[CreatedDate],
        c.[UpdatedDate],
        c.[IsDeleted]
    ORDER BY c.[CreatedDate] DESC;
END

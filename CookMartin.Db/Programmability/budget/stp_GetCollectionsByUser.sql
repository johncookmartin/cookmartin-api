CREATE PROCEDURE [budget].[stp_GetCollectionsByUser]
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT
        c.[CollectionId], c.[Name], c.[EmergencyFund], c.[OwnerId],
        c.[CreatedDate], c.[UpdatedDate], c.[IsDeleted]
    FROM [budget].[Collections] c
    LEFT JOIN [budget].[CollectionUsers] cu
        ON c.[CollectionId] = cu.[CollectionId]
        AND cu.[IsDeleted] = 0
    WHERE c.[IsDeleted] = 0
        AND (c.[OwnerId] = @UserId OR cu.[UserId] = @UserId)
    ORDER BY c.[CreatedDate] DESC;
END

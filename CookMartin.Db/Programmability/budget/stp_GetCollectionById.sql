CREATE PROCEDURE [budget].[stp_GetCollectionById]
    @CollectionId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        c.[CollectionId], c.[Name], c.[EmergencyFund], c.[OwnerId],
        c.[CreatedDate], c.[UpdatedDate], c.[IsDeleted],
        cu.[CollectionUserId], cu.[UserId]
    FROM [budget].[Collections] c
    LEFT JOIN [budget].[CollectionUsers] cu
        ON c.[CollectionId] = cu.[CollectionId]
        AND cu.[IsDeleted] = 0
    WHERE c.[CollectionId] = @CollectionId
        AND c.[IsDeleted] = 0;
END

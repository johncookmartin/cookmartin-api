CREATE PROCEDURE [oscar].[stp_GetCategoriesWithNominees]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.[CategoryId],
        c.[Name]         AS CategoryName,
        c.[DisplayOrder],
        n.[NomineeId],
        n.[Name]         AS NomineeName,
        n.[IsWinner]
    FROM [oscar].[Categories] c
    INNER JOIN [oscar].[Nominees] n ON n.[CategoryId] = c.[CategoryId]
    ORDER BY c.[DisplayOrder], c.[CategoryId], n.[NomineeId];
END

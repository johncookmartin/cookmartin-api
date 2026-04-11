CREATE PROCEDURE [budget].[stp_GetBudgetsByCollection]
    @CollectionId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [BudgetId], [Name], [Type], [StartingAmount], [CollectionId],
        [CreatedDate], [UpdatedDate], [IsDeleted]
    FROM [budget].[Budgets]
    WHERE [CollectionId] = @CollectionId
        AND [IsDeleted] = 0
    ORDER BY [CreatedDate] DESC;
END

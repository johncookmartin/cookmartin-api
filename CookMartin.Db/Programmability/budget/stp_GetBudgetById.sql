CREATE PROCEDURE [budget].[stp_GetBudgetById]
    @BudgetId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [BudgetId], [Name], [Type], [StartingAmount], [CollectionId],
        [CreatedDate], [UpdatedDate], [IsDeleted]
    FROM [budget].[Budgets]
    WHERE [BudgetId] = @BudgetId
        AND [IsDeleted] = 0;
END

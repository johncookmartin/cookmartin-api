CREATE PROCEDURE [budget].[stp_GetBudgetItemsByBudget]
    @BudgetId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [BudgetItemId], [Label], [BudgetedAmount], [DueDate],
        [ActualAmount], [ActualDate], [Type], [BudgetId], [UserId],
        [CreatedDate], [UpdatedDate], [IsDeleted]
    FROM [budget].[BudgetItems]
    WHERE [BudgetId] = @BudgetId
        AND [IsDeleted] = 0
    ORDER BY [CreatedDate] DESC;
END

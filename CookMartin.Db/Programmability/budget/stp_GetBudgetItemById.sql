CREATE PROCEDURE [budget].[stp_GetBudgetItemById]
    @BudgetItemId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [BudgetItemId], [Label], [BudgetedAmount], [DueDate],
        [ActualAmount], [ActualDate], [Type], [BudgetId], [UserId],
        [CreatedDate], [UpdatedDate], [IsDeleted]
    FROM [budget].[BudgetItems]
    WHERE [BudgetItemId] = @BudgetItemId
        AND [IsDeleted] = 0;
END

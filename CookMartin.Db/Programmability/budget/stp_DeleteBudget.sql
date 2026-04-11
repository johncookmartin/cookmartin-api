CREATE PROCEDURE [budget].[stp_DeleteBudget]
    @BudgetId INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @RowsAffected INT = 0;

    UPDATE [budget].[BudgetItems]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [BudgetId] = @BudgetId;
    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    UPDATE [budget].[Budgets]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [BudgetId] = @BudgetId;
    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    SELECT @RowsAffected AS RowsAffected;
END

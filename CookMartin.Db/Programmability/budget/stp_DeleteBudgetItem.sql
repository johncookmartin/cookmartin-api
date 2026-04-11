CREATE PROCEDURE [budget].[stp_DeleteBudgetItem]
    @BudgetItemId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [budget].[BudgetItems]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [BudgetItemId] = @BudgetItemId;
    SELECT @@ROWCOUNT AS RowsAffected;
END

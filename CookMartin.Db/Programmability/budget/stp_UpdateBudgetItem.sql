CREATE PROCEDURE [budget].[stp_UpdateBudgetItem]
    @BudgetItemId   INT,
    @Label          NVARCHAR(255),
    @BudgetedAmount DECIMAL(18, 2),
    @DueDate        DATETIME2,
    @ActualAmount   DECIMAL(18, 2),
    @ActualDate     DATETIME2,
    @Type           NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [budget].[BudgetItems]
    SET [Label]          = @Label,
        [BudgetedAmount] = @BudgetedAmount,
        [DueDate]        = @DueDate,
        [ActualAmount]   = @ActualAmount,
        [ActualDate]     = @ActualDate,
        [Type]           = @Type,
        [UpdatedDate]    = GETUTCDATE()
    WHERE [BudgetItemId] = @BudgetItemId
        AND [IsDeleted] = 0;
    SELECT @@ROWCOUNT AS RowsAffected;
END

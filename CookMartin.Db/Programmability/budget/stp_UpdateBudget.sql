CREATE PROCEDURE [budget].[stp_UpdateBudget]
    @BudgetId       INT,
    @Name           NVARCHAR(255),
    @Type           NVARCHAR(100),
    @StartingAmount DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [budget].[Budgets]
    SET [Name]           = @Name,
        [Type]           = @Type,
        [StartingAmount] = @StartingAmount,
        [UpdatedDate]    = GETUTCDATE()
    WHERE [BudgetId] = @BudgetId
        AND [IsDeleted] = 0;
    SELECT @@ROWCOUNT AS RowsAffected;
END

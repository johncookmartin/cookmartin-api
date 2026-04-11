CREATE PROCEDURE [budget].[stp_CreateBudgetItem]
    @BudgetId       INT,
    @UserId         NVARCHAR(450),
    @Label          NVARCHAR(255),
    @BudgetedAmount DECIMAL(18, 2),
    @DueDate        DATETIME2,
    @Type           NVARCHAR(100),
    @BudgetItemId   INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [budget].[BudgetItems]
        ([BudgetId], [UserId], [Label], [BudgetedAmount], [DueDate], [Type], [CreatedDate], [IsDeleted])
    VALUES
        (@BudgetId, @UserId, @Label, @BudgetedAmount, @DueDate, @Type, GETUTCDATE(), 0);
    SET @BudgetItemId = SCOPE_IDENTITY();
    SELECT @BudgetItemId AS BudgetItemId;
END

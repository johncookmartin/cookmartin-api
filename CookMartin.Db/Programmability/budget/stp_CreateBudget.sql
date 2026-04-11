CREATE PROCEDURE [budget].[stp_CreateBudget]
    @CollectionId  INT,
    @Name          NVARCHAR(255),
    @Type          NVARCHAR(100),
    @StartingAmount DECIMAL(18, 2),
    @BudgetId      INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [budget].[Budgets] ([CollectionId], [Name], [Type], [StartingAmount], [CreatedDate], [IsDeleted])
    VALUES (@CollectionId, @Name, @Type, @StartingAmount, GETUTCDATE(), 0);
    SET @BudgetId = SCOPE_IDENTITY();
    SELECT @BudgetId AS BudgetId;
END

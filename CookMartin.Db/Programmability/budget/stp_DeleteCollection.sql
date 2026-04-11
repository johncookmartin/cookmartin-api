CREATE PROCEDURE [budget].[stp_DeleteCollection]
    @CollectionId INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @RowsAffected INT = 0;

    UPDATE [budget].[BudgetItems]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [BudgetId] IN (
        SELECT [BudgetId] FROM [budget].[Budgets]
        WHERE [CollectionId] = @CollectionId
    );
    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    UPDATE [budget].[Budgets]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId;
    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    UPDATE [budget].[RecurringItems]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId;
    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    UPDATE [budget].[CollectionUsers]
    SET [IsDeleted] = 1
    WHERE [CollectionId] = @CollectionId;
    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    UPDATE [budget].[Collections]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId;
    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    SELECT @RowsAffected AS RowsAffected;
END

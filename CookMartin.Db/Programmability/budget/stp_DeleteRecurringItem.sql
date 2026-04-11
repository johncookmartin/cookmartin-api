CREATE PROCEDURE [budget].[stp_DeleteRecurringItem]
    @RecurringItemId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [budget].[RecurringItems]
    SET [IsDeleted]   = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [RecurringItemId] = @RecurringItemId;
    SELECT @@ROWCOUNT AS RowsAffected;
END

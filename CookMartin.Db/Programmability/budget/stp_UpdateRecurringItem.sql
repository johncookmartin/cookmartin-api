CREATE PROCEDURE [budget].[stp_UpdateRecurringItem]
    @RecurringItemId INT,
    @Label           NVARCHAR(255),
    @Amount          DECIMAL(18, 2),
    @Type            NVARCHAR(100),
    @IsShared        BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [budget].[RecurringItems]
    SET [Label]       = @Label,
        [Amount]      = @Amount,
        [Type]        = @Type,
        [IsShared]    = @IsShared,
        [UpdatedDate] = GETUTCDATE()
    WHERE [RecurringItemId] = @RecurringItemId
        AND [IsDeleted] = 0;
    SELECT @@ROWCOUNT AS RowsAffected;
END

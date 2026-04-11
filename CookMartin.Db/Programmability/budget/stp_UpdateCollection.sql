CREATE PROCEDURE [budget].[stp_UpdateCollection]
    @CollectionId  INT,
    @Name          NVARCHAR(255),
    @EmergencyFund DECIMAL(18, 2)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [budget].[Collections]
    SET [Name]          = @Name,
        [EmergencyFund] = @EmergencyFund,
        [UpdatedDate]   = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId
        AND [IsDeleted] = 0;
    SELECT @@ROWCOUNT AS RowsAffected;
END

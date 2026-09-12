CREATE PROCEDURE [portfolio].[stp_DeleteWorkHistory]
    @WorkHistoryId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RowsAffected INT;

    UPDATE [portfolio].[WorkHistory]
    SET [IsDeleted] = 1
    WHERE [WorkHistoryId] = @WorkHistoryId;

    SET @RowsAffected = @@ROWCOUNT;

    UPDATE [portfolio].[WorkHistoryBullets]
    SET [IsDeleted] = 1
    WHERE [WorkHistoryId] = @WorkHistoryId;

    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    SELECT @RowsAffected AS RowsAffected;
END

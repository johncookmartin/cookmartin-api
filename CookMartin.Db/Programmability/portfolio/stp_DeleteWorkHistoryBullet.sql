CREATE PROCEDURE [portfolio].[stp_DeleteWorkHistoryBullet]
    @WorkHistoryBulletId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[WorkHistoryBullets]
    SET [IsDeleted] = 1
    WHERE [WorkHistoryBulletId] = @WorkHistoryBulletId;

    SELECT @@ROWCOUNT AS RowsAffected;
END

CREATE PROCEDURE [portfolio].[stp_UpdateWorkHistoryBullet]
    @WorkHistoryBulletId INT,
    @BulletText NVARCHAR(500),
    @DisplayOrder INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[WorkHistoryBullets]
    SET [BulletText] = @BulletText,
        [DisplayOrder] = @DisplayOrder
    WHERE [WorkHistoryBulletId] = @WorkHistoryBulletId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END

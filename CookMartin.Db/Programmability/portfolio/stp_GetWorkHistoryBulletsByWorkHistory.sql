CREATE PROCEDURE [portfolio].[stp_GetWorkHistoryBulletsByWorkHistory]
    @WorkHistoryId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [WorkHistoryBulletId],
        [WorkHistoryId],
        [BulletText],
        [DisplayOrder]
    FROM [portfolio].[WorkHistoryBullets]
    WHERE [WorkHistoryId] = @WorkHistoryId
        AND [IsDeleted] = 0
    ORDER BY [DisplayOrder], [WorkHistoryBulletId];
END

CREATE PROCEDURE [portfolio].[stp_CreateWorkHistoryBullet]
    @WorkHistoryId INT,
    @BulletText NVARCHAR(500),
    @DisplayOrder INT,
    @WorkHistoryBulletId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [portfolio].[WorkHistoryBullets] ([WorkHistoryId], [BulletText], [DisplayOrder], [IsDeleted])
    VALUES (@WorkHistoryId, @BulletText, @DisplayOrder, 0);

    SET @WorkHistoryBulletId = SCOPE_IDENTITY();

    SELECT @WorkHistoryBulletId AS WorkHistoryBulletId;
END

CREATE PROCEDURE [portfolio].[stp_UpdateSkill]
    @SkillId INT,
    @SkillName NVARCHAR(100),
    @DisplayOrder INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[Skills]
    SET [SkillName] = @SkillName,
        [DisplayOrder] = @DisplayOrder
    WHERE [SkillId] = @SkillId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END

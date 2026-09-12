CREATE PROCEDURE [portfolio].[stp_DeleteSkill]
    @SkillId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[Skills]
    SET [IsDeleted] = 1
    WHERE [SkillId] = @SkillId;

    SELECT @@ROWCOUNT AS RowsAffected;
END

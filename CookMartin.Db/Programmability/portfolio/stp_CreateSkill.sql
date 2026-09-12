CREATE PROCEDURE [portfolio].[stp_CreateSkill]
    @ProfileId INT,
    @SkillName NVARCHAR(100),
    @DisplayOrder INT,
    @SkillId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [portfolio].[Skills] ([ProfileId], [SkillName], [DisplayOrder], [IsDeleted])
    VALUES (@ProfileId, @SkillName, @DisplayOrder, 0);

    SET @SkillId = SCOPE_IDENTITY();

    SELECT @SkillId AS SkillId;
END

CREATE PROCEDURE [portfolio].[stp_GetSkillsByProfile]
    @ProfileId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [SkillId],
        [ProfileId],
        [SkillName],
        [DisplayOrder]
    FROM [portfolio].[Skills]
    WHERE [ProfileId] = @ProfileId
        AND [IsDeleted] = 0
    ORDER BY [DisplayOrder], [SkillId];
END

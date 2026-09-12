CREATE PROCEDURE [portfolio].[stp_GetLinksByProfile]
    @ProfileId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [LinkId],
        [ProfileId],
        [Label],
        [Url],
        [DisplayOrder]
    FROM [portfolio].[Links]
    WHERE [ProfileId] = @ProfileId
        AND [IsDeleted] = 0
    ORDER BY [DisplayOrder], [LinkId];
END

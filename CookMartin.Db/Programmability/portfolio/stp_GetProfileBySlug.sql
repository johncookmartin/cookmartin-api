CREATE PROCEDURE [portfolio].[stp_GetProfileBySlug]
    @Slug NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [ProfileId],
        [Name],
        [Headline],
        [Bio],
        [Slug],
        [IsPublished],
        [CreatedDate],
        [UpdatedDate]
    FROM [portfolio].[Profiles]
    WHERE [Slug] = @Slug
        AND [IsPublished] = 1
        AND [IsDeleted] = 0;
END

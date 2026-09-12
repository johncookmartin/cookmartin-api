CREATE PROCEDURE [portfolio].[stp_GetProfileByUserId]
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [ProfileId],
        [UserId],
        [Name],
        [Headline],
        [Bio],
        [Slug],
        [IsPublished],
        [CreatedDate],
        [UpdatedDate]
    FROM [portfolio].[Profiles]
    WHERE [UserId] = @UserId
        AND [IsDeleted] = 0;
END

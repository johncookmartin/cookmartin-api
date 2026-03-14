CREATE PROCEDURE [oscar].[stp_GetLeaderboard]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.[UserName],
        COUNT(CASE WHEN n.[IsWinner] = 1 THEN 1 END) AS Score
    FROM [oscar].[Picks] p
    INNER JOIN [oscar].[Nominees] n ON n.[NomineeId] = p.[NomineeId]
    GROUP BY p.[UserName]
    ORDER BY Score DESC, p.[UserName] ASC;
END

CREATE PROCEDURE [oscar].[stp_GetUserResults]
    @UserName NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.[CategoryId],
        c.[Name]          AS CategoryName,
        c.[DisplayOrder],
        p.[NomineeId]     AS PickedNomineeId,
        pn.[Name]         AS PickedNomineeName,
        wn.[NomineeId]    AS WinnerNomineeId,
        wn.[Name]         AS WinnerNomineeName,
        CASE WHEN p.[NomineeId] = wn.[NomineeId] THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsCorrect
    FROM [oscar].[Categories] c
    LEFT JOIN [oscar].[Picks] p
        ON p.[CategoryId] = c.[CategoryId] AND p.[UserName] = @UserName
    LEFT JOIN [oscar].[Nominees] pn
        ON pn.[NomineeId] = p.[NomineeId]
    LEFT JOIN [oscar].[Nominees] wn
        ON wn.[CategoryId] = c.[CategoryId] AND wn.[IsWinner] = 1
    ORDER BY c.[DisplayOrder], c.[CategoryId];
END

CREATE PROCEDURE [oscar].[stp_GetSubmissions]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT [UserName]
    FROM [oscar].[Picks]
    ORDER BY [UserName] ASC;
END

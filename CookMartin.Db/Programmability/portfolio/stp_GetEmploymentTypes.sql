CREATE PROCEDURE [portfolio].[stp_GetEmploymentTypes]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [EmploymentTypeId],
        [Name]
    FROM [portfolio].[EmploymentTypes]
    ORDER BY [Name];
END

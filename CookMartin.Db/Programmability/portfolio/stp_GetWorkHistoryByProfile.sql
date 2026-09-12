CREATE PROCEDURE [portfolio].[stp_GetWorkHistoryByProfile]
    @ProfileId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [WorkHistoryId],
        [ProfileId],
        [Company],
        [Title],
        [EmploymentTypeId],
        [Location],
        [StartDate],
        [EndDate],
        [DisplayOrder]
    FROM [portfolio].[WorkHistory]
    WHERE [ProfileId] = @ProfileId
        AND [IsDeleted] = 0
    ORDER BY [DisplayOrder], [WorkHistoryId];
END

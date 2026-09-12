CREATE PROCEDURE [portfolio].[stp_UpdateWorkHistory]
    @WorkHistoryId INT,
    @Company NVARCHAR(255),
    @Title NVARCHAR(255),
    @EmploymentTypeId INT,
    @Location NVARCHAR(255),
    @StartDate DATE,
    @EndDate DATE = NULL,
    @DisplayOrder INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[WorkHistory]
    SET [Company] = @Company,
        [Title] = @Title,
        [EmploymentTypeId] = @EmploymentTypeId,
        [Location] = @Location,
        [StartDate] = @StartDate,
        [EndDate] = @EndDate,
        [DisplayOrder] = @DisplayOrder
    WHERE [WorkHistoryId] = @WorkHistoryId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END

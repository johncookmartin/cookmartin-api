CREATE PROCEDURE [portfolio].[stp_CreateWorkHistory]
    @ProfileId INT,
    @Company NVARCHAR(255),
    @Title NVARCHAR(255),
    @EmploymentTypeId INT,
    @Location NVARCHAR(255),
    @StartDate DATE,
    @EndDate DATE = NULL,
    @DisplayOrder INT,
    @WorkHistoryId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [portfolio].[WorkHistory] ([ProfileId], [Company], [Title], [EmploymentTypeId], [Location], [StartDate], [EndDate], [DisplayOrder], [IsDeleted])
    VALUES (@ProfileId, @Company, @Title, @EmploymentTypeId, @Location, @StartDate, @EndDate, @DisplayOrder, 0);

    SET @WorkHistoryId = SCOPE_IDENTITY();

    SELECT @WorkHistoryId AS WorkHistoryId;
END

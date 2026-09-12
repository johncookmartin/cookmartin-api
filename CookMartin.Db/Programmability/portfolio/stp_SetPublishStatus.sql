CREATE PROCEDURE [portfolio].[stp_SetPublishStatus]
    @ProfileId INT,
    @IsPublished BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[Profiles]
    SET [IsPublished] = @IsPublished,
        [UpdatedDate] = GETUTCDATE()
    WHERE [ProfileId] = @ProfileId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END

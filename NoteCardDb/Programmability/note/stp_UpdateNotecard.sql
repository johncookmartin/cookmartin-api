CREATE PROCEDURE [note].[stp_UpdateNotecard]
    @NotecardId INT,
    @FrontDescription NVARCHAR(MAX),
    @BackDescription NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [note].[Notecards]
    SET [FrontDescription] = @FrontDescription,
        [BackDescription] = @BackDescription,
        [UpdatedDate] = GETUTCDATE()
    WHERE [NotecardId] = @NotecardId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END

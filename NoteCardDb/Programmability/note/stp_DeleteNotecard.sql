CREATE PROCEDURE [note].[stp_DeleteNotecard]
    @NotecardId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [note].[Notecards]
    SET [IsDeleted] = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [NotecardId] = @NotecardId;

    SELECT @@ROWCOUNT AS RowsAffected;
END

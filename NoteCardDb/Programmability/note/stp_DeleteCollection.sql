CREATE PROCEDURE [note].[stp_DeleteCollection]
    @CollectionId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [note].[Collections]
    SET [IsDeleted] = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId;

    SELECT @@ROWCOUNT AS RowsAffected;
END

CREATE PROCEDURE [note].[stp_DeleteCollection]
    @CollectionId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RowsAffected INT;

    UPDATE [note].[Collections]
    SET [IsDeleted] = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId;

    SET @RowsAffected = @@ROWCOUNT;

    UPDATE [note].[Notecards]
    SET [IsDeleted] = 1,
        [UpdatedDate] = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId;

    SET @RowsAffected = @RowsAffected + @@ROWCOUNT;

    SELECT @RowsAffected AS RowsAffected;
END

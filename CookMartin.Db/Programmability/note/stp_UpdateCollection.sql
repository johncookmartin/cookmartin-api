CREATE PROCEDURE [note].[stp_UpdateCollection]
    @CollectionId INT,
    @Name NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [note].[Collections]
    SET [Name] = @Name,
        [UpdatedDate] = GETUTCDATE()
    WHERE [CollectionId] = @CollectionId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END
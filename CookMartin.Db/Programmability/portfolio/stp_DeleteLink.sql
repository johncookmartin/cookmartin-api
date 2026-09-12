CREATE PROCEDURE [portfolio].[stp_DeleteLink]
    @LinkId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[Links]
    SET [IsDeleted] = 1
    WHERE [LinkId] = @LinkId;

    SELECT @@ROWCOUNT AS RowsAffected;
END

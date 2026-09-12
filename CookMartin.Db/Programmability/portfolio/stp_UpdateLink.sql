CREATE PROCEDURE [portfolio].[stp_UpdateLink]
    @LinkId INT,
    @Label NVARCHAR(100),
    @Url NVARCHAR(2048),
    @DisplayOrder INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[Links]
    SET [Label] = @Label,
        [Url] = @Url,
        [DisplayOrder] = @DisplayOrder
    WHERE [LinkId] = @LinkId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END

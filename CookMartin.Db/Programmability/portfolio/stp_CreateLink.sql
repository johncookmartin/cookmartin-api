CREATE PROCEDURE [portfolio].[stp_CreateLink]
    @ProfileId INT,
    @Label NVARCHAR(100),
    @Url NVARCHAR(2048),
    @DisplayOrder INT,
    @LinkId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [portfolio].[Links] ([ProfileId], [Label], [Url], [DisplayOrder], [IsDeleted])
    VALUES (@ProfileId, @Label, @Url, @DisplayOrder, 0);

    SET @LinkId = SCOPE_IDENTITY();

    SELECT @LinkId AS LinkId;
END

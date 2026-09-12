CREATE PROCEDURE [portfolio].[stp_CreateProfile]
    @UserId NVARCHAR(450),
    @Name NVARCHAR(255),
    @Headline NVARCHAR(255) = NULL,
    @Bio NVARCHAR(MAX) = NULL,
    @Slug NVARCHAR(255),
    @ProfileId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [portfolio].[Profiles] ([UserId], [Name], [Headline], [Bio], [Slug], [IsPublished], [CreatedDate], [IsDeleted])
    VALUES (@UserId, @Name, @Headline, @Bio, @Slug, 0, GETUTCDATE(), 0);

    SET @ProfileId = SCOPE_IDENTITY();

    SELECT @ProfileId AS ProfileId;
END

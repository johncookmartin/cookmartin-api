CREATE PROCEDURE [note].[stp_CreateNotecard]
    @CollectionId INT,
    @FrontDescription NVARCHAR(MAX),
    @BackDescription NVARCHAR(MAX),
    @NotecardId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [note].[Notecards] ([CollectionId], [FrontDescription], [BackDescription], [CreatedDate], [IsDeleted])
    VALUES (@CollectionId, @FrontDescription, @BackDescription, GETUTCDATE(), 0);

    SET @NotecardId = SCOPE_IDENTITY();

    SELECT @NotecardId AS NotecardId;
END

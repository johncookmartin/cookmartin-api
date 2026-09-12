CREATE PROCEDURE [portfolio].[stp_UpdateProfile]
    @ProfileId INT,
    @Name NVARCHAR(255),
    @Headline NVARCHAR(255) = NULL,
    @Bio NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [portfolio].[Profiles]
    SET [Name] = @Name,
        [Headline] = @Headline,
        [Bio] = @Bio,
        [UpdatedDate] = GETUTCDATE()
    WHERE [ProfileId] = @ProfileId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected;
END

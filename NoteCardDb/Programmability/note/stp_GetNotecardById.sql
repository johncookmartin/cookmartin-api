CREATE PROCEDURE [note].[stp_GetNotecardById]
    @NotecardId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [NotecardId],
        [CollectionId],
        [FrontDescription],
        [BackDescription],
        [CreatedDate],
        [UpdatedDate],
        [IsDeleted]
    FROM [note].[Notecards]
    WHERE [NotecardId] = @NotecardId
        AND [IsDeleted] = 0;
END

CREATE PROCEDURE [note].[stp_GetNotecardsByCollection]
    @CollectionId INT
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
    WHERE [CollectionId] = @CollectionId
        AND [IsDeleted] = 0
    ORDER BY [CreatedDate] ASC;
END

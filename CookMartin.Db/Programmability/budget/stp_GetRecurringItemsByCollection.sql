CREATE PROCEDURE [budget].[stp_GetRecurringItemsByCollection]
    @CollectionId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [RecurringItemId], [Label], [Amount], [Type], [IsShared],
        [UserId], [CollectionId], [CreatedDate], [UpdatedDate], [IsDeleted]
    FROM [budget].[RecurringItems]
    WHERE [CollectionId] = @CollectionId
        AND [IsDeleted] = 0
    ORDER BY [CreatedDate] DESC;
END

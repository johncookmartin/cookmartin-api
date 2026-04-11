CREATE PROCEDURE [budget].[stp_GetRecurringItemById]
    @RecurringItemId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        [RecurringItemId], [Label], [Amount], [Type], [IsShared],
        [UserId], [CollectionId], [CreatedDate], [UpdatedDate], [IsDeleted]
    FROM [budget].[RecurringItems]
    WHERE [RecurringItemId] = @RecurringItemId
        AND [IsDeleted] = 0;
END

CREATE PROCEDURE [note].[stp_GetQuizzesByUser]
    @UserId NVARCHAR(450)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        q.[QuizId],
        q.[CollectionId],
        c.[Name] AS CollectionName,
        q.[UserId],
        q.[QuizDate],
        q.[Status],
        q.[Score],
        q.[CreatedDate],
        q.[IsDeleted]
    FROM [note].[Quizzes] q
    INNER JOIN [note].[Collections] c ON q.[CollectionId] = c.[CollectionId]
    WHERE q.[UserId] = @UserId
        AND q.[IsDeleted] = 0
    ORDER BY q.[QuizDate] DESC;
END

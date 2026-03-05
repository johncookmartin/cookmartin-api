CREATE PROCEDURE [note].[stp_GetQuizById]
    @QuizId INT
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
    WHERE q.[QuizId] = @QuizId
        AND q.[IsDeleted] = 0;

    SELECT 
        qi.[QuizInstanceId],
        qi.[QuizId],
        qi.[NotecardId],
        n.[FrontDescription],
        n.[BackDescription],
        qi.[IsCorrect],
        qi.[AnsweredDate]
    FROM [note].[QuizInstances] qi
    INNER JOIN [note].[Notecards] n ON qi.[NotecardId] = n.[NotecardId]
    WHERE qi.[QuizId] = @QuizId
    ORDER BY qi.[QuizInstanceId];
END

CREATE PROCEDURE [note].[stp_GetQuizInstancesByQuiz]
    @QuizId INT
AS
BEGIN
    SET NOCOUNT ON;

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

CREATE PROCEDURE [note].[stp_GetQuizInstancesByQuiz]
    @QuizId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        qi.[QuizInstanceId],
        qi.[QuizId],
        qi.[NotecardId],
        qi.[IsCorrect],
        qi.[AnsweredDate]
    FROM [note].[QuizInstances] qi
    WHERE qi.[QuizId] = @QuizId
    ORDER BY qi.[QuizInstanceId];
END

CREATE PROCEDURE [note].[stp_CompleteQuiz]
    @QuizId INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Score INT;

    SELECT @Score = COUNT(*)
    FROM [note].[QuizInstances]
    WHERE [QuizId] = @QuizId
        AND [IsCorrect] = 1;

    UPDATE [note].[Quizzes]
    SET [Status] = 'Completed',
        [Score] = @Score
    WHERE [QuizId] = @QuizId
        AND [IsDeleted] = 0;

    SELECT @@ROWCOUNT AS RowsAffected, @Score AS Score;
END

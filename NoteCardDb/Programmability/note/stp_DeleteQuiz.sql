CREATE PROCEDURE [note].[stp_DeleteQuiz]
    @QuizId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [note].[Quizzes]
    SET [IsDeleted] = 1
    WHERE [QuizId] = @QuizId;

    SELECT @@ROWCOUNT AS RowsAffected;
END

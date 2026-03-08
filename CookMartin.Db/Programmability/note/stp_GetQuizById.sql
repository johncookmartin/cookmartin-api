CREATE PROCEDURE [note].[stp_GetQuizById]
    @QuizId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        q.[QuizId],
        q.[CollectionId],
        q.[UserId],
        q.[QuizDate],
        q.[Status],
        q.[Score],
        q.[CreatedDate],
        q.[IsDeleted]
    FROM [note].[Quizzes] q
    WHERE q.[QuizId] = @QuizId
        AND q.[IsDeleted] = 0;
END

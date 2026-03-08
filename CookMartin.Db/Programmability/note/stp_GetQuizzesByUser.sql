CREATE PROCEDURE [note].[stp_GetQuizzesByUser]
    @UserId NVARCHAR(450)
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
    WHERE q.[UserId] = @UserId
        AND q.[IsDeleted] = 0
    ORDER BY q.[QuizDate] DESC;
END

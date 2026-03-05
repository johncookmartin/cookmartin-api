CREATE PROCEDURE [note].[stp_CreateQuiz]
    @CollectionId INT,
    @UserId NVARCHAR(450),
    @QuizId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO [note].[Quizzes] ([CollectionId], [UserId], [QuizDate], [Status], [CreatedDate], [IsDeleted])
        VALUES (@CollectionId, @UserId, GETUTCDATE(), 'InProgress', GETUTCDATE(), 0);

        SET @QuizId = SCOPE_IDENTITY();

        INSERT INTO [note].[QuizInstances] ([QuizId], [NotecardId], [IsCorrect], [AnsweredDate])
        SELECT @QuizId, [NotecardId], NULL, NULL
        FROM [note].[Notecards]
        WHERE [CollectionId] = @CollectionId
            AND [IsDeleted] = 0;

        COMMIT TRANSACTION;

        SELECT @QuizId AS QuizId;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

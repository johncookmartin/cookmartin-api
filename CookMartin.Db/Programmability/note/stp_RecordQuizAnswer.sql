CREATE PROCEDURE [note].[stp_RecordQuizAnswer]
    @NotecardId INT,
    @QuizId INT,
    @IsCorrect BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [note].[QuizInstances]
    SET [IsCorrect] = @IsCorrect,
        [AnsweredDate] = GETUTCDATE()
    WHERE [NotecardId] = @NotecardId AND [QuizId] = @QuizId AND [AnsweredDate] IS NULL;

    SELECT @@ROWCOUNT AS RowsAffected;
END

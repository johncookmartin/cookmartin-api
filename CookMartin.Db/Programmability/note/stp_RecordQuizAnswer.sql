CREATE PROCEDURE [note].[stp_RecordQuizAnswer]
    @QuizInstanceId INT,
    @IsCorrect BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [note].[QuizInstances]
    SET [IsCorrect] = @IsCorrect,
        [AnsweredDate] = GETUTCDATE()
    WHERE [QuizInstanceId] = @QuizInstanceId;

    SELECT @@ROWCOUNT AS RowsAffected;
END

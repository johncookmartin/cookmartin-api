CREATE TABLE [note].[QuizInstances]
(
    [QuizInstanceId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [QuizId] INT NOT NULL,
    [NotecardId] INT NOT NULL,
    [IsCorrect] BIT NULL,
    [AnsweredDate] DATETIME2 NULL,
    CONSTRAINT [FK_QuizInstances_Quizzes] FOREIGN KEY ([QuizId]) REFERENCES [note].[Quizzes]([QuizId]),
    CONSTRAINT [FK_QuizInstances_Notecards] FOREIGN KEY ([NotecardId]) REFERENCES [note].[Notecards]([NotecardId])
);

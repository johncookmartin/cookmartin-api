CREATE TABLE [note].[Quizzes]
(
    [QuizId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CollectionId] INT NOT NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    [QuizDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [Status] NVARCHAR(50) NOT NULL,
    [Score] INT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Quizzes_Collections] FOREIGN KEY ([CollectionId]) REFERENCES [note].[Collections]([CollectionId]),
    CONSTRAINT [CK_Quizzes_Status] CHECK ([Status] IN ('InProgress', 'Completed'))
);

CREATE TABLE [note].[Notecards]
(
    [NotecardId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CollectionId] INT NOT NULL,
    [FrontDescription] NVARCHAR(MAX) NOT NULL,
    [BackDescription] NVARCHAR(MAX) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate] DATETIME2 NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Notecards_Collections] FOREIGN KEY ([CollectionId]) REFERENCES [note].[Collections]([CollectionId])
);

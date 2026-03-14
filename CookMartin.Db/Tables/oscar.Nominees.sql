CREATE TABLE [oscar].[Nominees]
(
    [NomineeId]   INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CategoryId]  INT           NOT NULL,
    [Name]        NVARCHAR(300) NOT NULL,
    [IsWinner]    BIT           NOT NULL DEFAULT 0,
    [WonDate]     DATETIME2     NULL,
    [CreatedDate] DATETIME2     NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [FK_Nominees_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [oscar].[Categories]([CategoryId])
);

GO

CREATE NONCLUSTERED INDEX [IX_Nominees_CategoryId] ON [oscar].[Nominees] ([CategoryId]);

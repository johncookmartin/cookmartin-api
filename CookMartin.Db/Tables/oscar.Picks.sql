CREATE TABLE [oscar].[Picks]
(
    [PickId]      INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserName]    NVARCHAR(200) NOT NULL,
    [CategoryId]  INT           NOT NULL,
    [NomineeId]   INT           NOT NULL,
    [PickedDate]  DATETIME2     NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [FK_Picks_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [oscar].[Categories]([CategoryId]),
    CONSTRAINT [FK_Picks_Nominees]   FOREIGN KEY ([NomineeId])  REFERENCES [oscar].[Nominees]([NomineeId]),
    CONSTRAINT [UQ_Picks_UserCategory] UNIQUE ([UserName], [CategoryId])
);

GO

CREATE NONCLUSTERED INDEX [IX_Picks_UserName] ON [oscar].[Picks] ([UserName]);

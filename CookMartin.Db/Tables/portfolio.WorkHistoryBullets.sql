CREATE TABLE [portfolio].[WorkHistoryBullets]
(
    [WorkHistoryBulletId] INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [WorkHistoryId]       INT           NOT NULL,
    [BulletText]          NVARCHAR(500) NOT NULL,
    [DisplayOrder]        INT           NOT NULL,
    [IsDeleted]           BIT           NOT NULL DEFAULT 0,
    CONSTRAINT [FK_WorkHistoryBullets_WorkHistory] FOREIGN KEY ([WorkHistoryId]) REFERENCES [portfolio].[WorkHistory]([WorkHistoryId])
);

GO

CREATE NONCLUSTERED INDEX [IX_WorkHistoryBullets_WorkHistoryId] ON [portfolio].[WorkHistoryBullets] ([WorkHistoryId]);

CREATE TABLE [portfolio].[Skills]
(
    [SkillId]      INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ProfileId]    INT           NOT NULL,
    [SkillName]    NVARCHAR(100) NOT NULL,
    [DisplayOrder] INT           NOT NULL,
    [IsDeleted]    BIT           NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Skills_Profiles] FOREIGN KEY ([ProfileId]) REFERENCES [portfolio].[Profiles]([ProfileId])
);

GO

CREATE NONCLUSTERED INDEX [IX_Skills_ProfileId] ON [portfolio].[Skills] ([ProfileId]);

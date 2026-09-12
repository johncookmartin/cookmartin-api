CREATE TABLE [portfolio].[Links]
(
    [LinkId]       INT            IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ProfileId]    INT            NOT NULL,
    [Label]        NVARCHAR(100)  NOT NULL,
    [Url]          NVARCHAR(2048) NOT NULL,
    [DisplayOrder] INT            NOT NULL,
    [IsDeleted]    BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Links_Profiles] FOREIGN KEY ([ProfileId]) REFERENCES [portfolio].[Profiles]([ProfileId])
);

GO

CREATE NONCLUSTERED INDEX [IX_Links_ProfileId] ON [portfolio].[Links] ([ProfileId]);

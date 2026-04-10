CREATE TABLE [budget].[RecurringItems]
(
    [RecurringItemId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Label]           NVARCHAR(255) NOT NULL,
    [Amount]          DECIMAL(18, 2) NOT NULL,
    [Type]            NVARCHAR(100) NOT NULL,
    [IsShared]        BIT NOT NULL DEFAULT 0,
    [UserId]          NVARCHAR(450) NOT NULL,
    [CollectionId]    INT NOT NULL,
    [CreatedDate]     DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate]     DATETIME2 NULL,
    [IsDeleted]       BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_RecurringItems_Collections] FOREIGN KEY ([CollectionId]) REFERENCES [budget].[Collections]([CollectionId])
);

GO

CREATE NONCLUSTERED INDEX [IX_budget_RecurringItems_CollectionId_IsActive] ON [budget].[RecurringItems] ([CollectionId])
WHERE [IsDeleted] = 0;

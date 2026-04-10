CREATE TABLE [budget].[Budgets]
(
    [BudgetId]       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name]           NVARCHAR(255) NOT NULL,
    [Type]           NVARCHAR(100) NOT NULL,
    [StartingAmount] DECIMAL(18, 2) NOT NULL,
    [CollectionId]   INT NOT NULL,
    [CreatedDate]    DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate]    DATETIME2 NULL,
    [IsDeleted]      BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Budgets_Collections] FOREIGN KEY ([CollectionId]) REFERENCES [budget].[Collections]([CollectionId]),
    CONSTRAINT [UQ_Budgets_CollectionId_Name] UNIQUE ([CollectionId], [Name])
);

GO

CREATE NONCLUSTERED INDEX [IX_budget_Budgets_CollectionId_IsActive] ON [budget].[Budgets] ([CollectionId])
WHERE [IsDeleted] = 0;

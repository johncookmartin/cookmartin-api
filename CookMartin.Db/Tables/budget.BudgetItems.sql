CREATE TABLE [budget].[BudgetItems]
(
    [BudgetItemId]   INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Label]          NVARCHAR(255) NOT NULL,
    [BudgetedAmount] DECIMAL(18, 2) NOT NULL,
    [DueDate]        DATETIME2 NULL,
    [ActualAmount]   DECIMAL(18, 2) NULL,
    [ActualDate]     DATETIME2 NULL,
    [Type]           NVARCHAR(100) NOT NULL,
    [BudgetId]       INT NOT NULL,
    [UserId]         NVARCHAR(450) NOT NULL,
    [CreatedDate]    DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate]    DATETIME2 NULL,
    [IsDeleted]      BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_BudgetItems_Budgets] FOREIGN KEY ([BudgetId]) REFERENCES [budget].[Budgets]([BudgetId])
);

GO

CREATE NONCLUSTERED INDEX [IX_budget_BudgetItems_BudgetId_IsActive] ON [budget].[BudgetItems] ([BudgetId])
WHERE [IsDeleted] = 0;

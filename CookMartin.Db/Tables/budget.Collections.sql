CREATE TABLE [budget].[Collections]
(
    [CollectionId]  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name]          NVARCHAR(255) NOT NULL,
    [EmergencyFund] DECIMAL(18, 2) NOT NULL,
    [OwnerId]       NVARCHAR(450) NOT NULL,
    [CreatedDate]   DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate]   DATETIME2 NULL,
    [IsDeleted]     BIT NOT NULL DEFAULT 0,
    CONSTRAINT [UQ_Collections_OwnerId_Name] UNIQUE ([OwnerId], [Name])
);

GO

CREATE INDEX [IX_budget_Collections_OwnerId_IsActive] ON [budget].[Collections] ([OwnerId])
WHERE [IsDeleted] = 0;

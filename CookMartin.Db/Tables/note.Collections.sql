CREATE TABLE [note].[Collections]
(
    [CollectionId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate] DATETIME2 NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0
);


GO

CREATE INDEX [IX_Collections_Name_UserId_IsActive] ON [note].[Collections] ([Name], [UserId])
WHERE [IsDeleted] = 0;
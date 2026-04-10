CREATE TABLE [budget].[CollectionUsers]
(
    [CollectionUserId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CollectionId]     INT NOT NULL,
    [UserId]           NVARCHAR(450) NOT NULL,
    [CreatedDate]      DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [IsDeleted]        BIT NOT NULL DEFAULT 0,
    CONSTRAINT [FK_CollectionUsers_Collections] FOREIGN KEY ([CollectionId]) REFERENCES [budget].[Collections]([CollectionId]),
    CONSTRAINT [UQ_CollectionUsers_CollectionId_UserId] UNIQUE ([CollectionId], [UserId])
);

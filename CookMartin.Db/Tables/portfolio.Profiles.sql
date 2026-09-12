CREATE TABLE [portfolio].[Profiles]
(
    [ProfileId]   INT             IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId]      NVARCHAR(450)   NOT NULL,
    [Name]        NVARCHAR(255)   NOT NULL,
    [Headline]    NVARCHAR(255)   NULL,
    [Bio]         NVARCHAR(MAX)   NULL,
    [Slug]        NVARCHAR(255)   NOT NULL,
    [IsPublished] BIT             NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME2       NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedDate] DATETIME2       NULL,
    [IsDeleted]   BIT             NOT NULL DEFAULT 0
);

GO

CREATE UNIQUE INDEX [UX_Profiles_UserId] ON [portfolio].[Profiles] ([UserId])
WHERE [IsDeleted] = 0;

GO

CREATE UNIQUE INDEX [UX_Profiles_Slug] ON [portfolio].[Profiles] ([Slug])
WHERE [IsDeleted] = 0;

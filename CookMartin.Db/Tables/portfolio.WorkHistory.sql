CREATE TABLE [portfolio].[WorkHistory]
(
    [WorkHistoryId]    INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ProfileId]        INT           NOT NULL,
    [Company]          NVARCHAR(255) NOT NULL,
    [Title]            NVARCHAR(255) NOT NULL,
    [EmploymentTypeId] INT           NOT NULL,
    [Location]         NVARCHAR(255) NOT NULL,
    [StartDate]        DATE          NOT NULL,
    [EndDate]          DATE          NULL,
    [DisplayOrder]     INT           NOT NULL,
    [IsDeleted]        BIT           NOT NULL DEFAULT 0,
    CONSTRAINT [FK_WorkHistory_Profiles] FOREIGN KEY ([ProfileId]) REFERENCES [portfolio].[Profiles]([ProfileId]),
    CONSTRAINT [FK_WorkHistory_EmploymentTypes] FOREIGN KEY ([EmploymentTypeId]) REFERENCES [portfolio].[EmploymentTypes]([EmploymentTypeId])
);

GO

CREATE NONCLUSTERED INDEX [IX_WorkHistory_ProfileId] ON [portfolio].[WorkHistory] ([ProfileId]);

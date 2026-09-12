CREATE TABLE [portfolio].[EmploymentTypes]
(
    [EmploymentTypeId] INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name]              NVARCHAR(50) NOT NULL,
    [CreatedDate]       DATETIME2    NOT NULL DEFAULT GETUTCDATE()
);

GO

CREATE UNIQUE INDEX [UX_EmploymentTypes_Name] ON [portfolio].[EmploymentTypes] ([Name]);

CREATE TABLE [oscar].[Categories]
(
    [CategoryId]   INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name]         NVARCHAR(200) NOT NULL,
    [DisplayOrder] INT           NOT NULL DEFAULT 0,
    [CreatedDate]  DATETIME2     NOT NULL DEFAULT GETUTCDATE()
);

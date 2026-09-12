-- Portfolio Employment Types Seed
-- Guarded per-row by name so a later manual INSERT of a 7th value
-- is never touched or duplicated by a re-run of this script.

IF NOT EXISTS (SELECT 1 FROM [portfolio].[EmploymentTypes] WHERE [Name] = 'Full-time')
    INSERT INTO [portfolio].[EmploymentTypes] ([Name]) VALUES ('Full-time');

IF NOT EXISTS (SELECT 1 FROM [portfolio].[EmploymentTypes] WHERE [Name] = 'Part-time')
    INSERT INTO [portfolio].[EmploymentTypes] ([Name]) VALUES ('Part-time');

IF NOT EXISTS (SELECT 1 FROM [portfolio].[EmploymentTypes] WHERE [Name] = 'Contract')
    INSERT INTO [portfolio].[EmploymentTypes] ([Name]) VALUES ('Contract');

IF NOT EXISTS (SELECT 1 FROM [portfolio].[EmploymentTypes] WHERE [Name] = 'Internship')
    INSERT INTO [portfolio].[EmploymentTypes] ([Name]) VALUES ('Internship');

IF NOT EXISTS (SELECT 1 FROM [portfolio].[EmploymentTypes] WHERE [Name] = 'Freelance')
    INSERT INTO [portfolio].[EmploymentTypes] ([Name]) VALUES ('Freelance');

IF NOT EXISTS (SELECT 1 FROM [portfolio].[EmploymentTypes] WHERE [Name] = 'Self-employed')
    INSERT INTO [portfolio].[EmploymentTypes] ([Name]) VALUES ('Self-employed');

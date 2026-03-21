/*
  Grant EXECUTE on all project schemas to [asp-cookmartin].
  Only runs if the principal exists (prod database only).
  Safe to re-run: skips schemas that already have the permission.
*/

IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'asp-cookmartin')
BEGIN
    DECLARE @SchemaName NVARCHAR(128);
    DECLARE @Sql        NVARCHAR(MAX);

    DECLARE schema_cursor CURSOR FOR
        SELECT s.name
        FROM sys.schemas s
        WHERE s.name NOT IN (
            'dbo', 'guest', 'INFORMATION_SCHEMA', 'sys',
            'db_owner', 'db_accessadmin', 'db_securityadmin', 'db_ddladmin',
            'db_backupoperator', 'db_datareader', 'db_datawriter',
            'db_denydatareader', 'db_denydatawriter'
        )
          AND NOT EXISTS (
              SELECT 1
              FROM sys.database_permissions dp
              JOIN sys.database_principals  pr ON dp.grantee_principal_id = pr.principal_id
              WHERE dp.class           = 3           -- SCHEMA
                AND dp.major_id        = s.schema_id
                AND dp.permission_name = 'EXECUTE'
                AND dp.state           IN ('G', 'W') -- GRANT or GRANT WITH GRANT OPTION
                AND pr.name            = 'asp-cookmartin'
          );

    OPEN schema_cursor;
    FETCH NEXT FROM schema_cursor INTO @SchemaName;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @Sql = N'GRANT EXECUTE ON SCHEMA::' + QUOTENAME(@SchemaName) + N' TO [asp-cookmartin];';
        EXEC sp_executesql @Sql;
        FETCH NEXT FROM schema_cursor INTO @SchemaName;
    END

    CLOSE schema_cursor;
    DEALLOCATE schema_cursor;
END

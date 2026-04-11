# CookMartin.Db

SQL Server Database project (SSDT). Owns all schema definitions and stored procedures. No C# code — deployed via DACPAC.

## Folder Structure

```
CookMartin.Db/
├── Security/
│   ├── note.sql             # CREATE SCHEMA note
│   ├── oscar.sql            # CREATE SCHEMA oscar
│   └── budget.sql           # CREATE SCHEMA budget
├── Tables/
│   ├── note.*               # Collections, Notecards, Quizzes, QuizInstances
│   ├── oscar.*              # Categories, Nominees, Picks
│   └── budget.*             # Collections, Budgets, BudgetItems, RecurringItems, CollectionUsers
├── Programmability/
│   ├── note/                # ~20 stored procedures for NoteCard
│   ├── oscar/               # ~7 stored procedures for ballot management
│   └── budget/              # ~20 stored procedures for budget management
├── Script.PostDeployment.sql
├── seed_oscar_2026.sql      # Seeds 98th Academy Awards nominees (24 categories)
└── Collections.sql
```

## Schema-to-Domain Mapping

| SQL Schema | Application Domain |
|------------|--------------------|
| `note`     | NoteCard           |
| `oscar`    | Oscar              |
| `budget`   | Budget             |

## Stored Procedure Naming Convention

```
{schema}.stp_{Verb}{Entity}
```

Examples:
- `note.stp_CreateCollection`
- `note.stp_GetCollectionsByUser`
- `note.stp_UpdateNotecard`
- `note.stp_DeleteQuiz`
- `note.stp_SeedGuest`
- `oscar.stp_UpsertPick`
- `oscar.stp_SetWinner`
- `oscar.stp_GetCategoriesWithNominees`
- `budget.stp_GetBudgetsByCollection`

## Output Parameter Convention

INSERT stored procedures return the new row's identity via an OUTPUT parameter:

```sql
@CollectionId INT OUTPUT
-- or
@NotecardId INT OUTPUT
```

The C# repositories read this via Dapper `DynamicParameters`:

```csharp
var p = new DynamicParameters();
p.Add("@CollectionId", dbType: DbType.Int32, direction: ParameterDirection.Output);
// ... after execute:
return p.Get<int>("@CollectionId");
```

UPDATE/DELETE procedures return `RowsAffected` as a scalar or result set.

## Post-Deployment Script

`Script.PostDeployment.sql` runs after every DACPAC deployment. Used for seed data or idempotent setup steps.

`seed_oscar_2026.sql` seeds the 98th Academy Awards nominees across 24 categories. Run manually or included in the post-deployment script.

## NoteCardDb

A secondary SSDT project at the solution root (`NoteCardDb/`). Likely a legacy or development database project kept separately from the main `CookMartin.Db`. Refer to its own files for schema details.

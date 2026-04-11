# CookMartin.Data

Shared persistence layer. Provides the Unit of Work + Repository pattern over Dapper + SQL Server. All feature repositories live here. No business logic.

## Folder Structure

```
CookMartin.Data/
├── DependencyInjection.cs
├── Interfaces/
│   ├── IConnectionFactory.cs
│   ├── IReadDb.cs
│   ├── IWriteDb.cs
│   ├── IUnitOfWork.cs
│   ├── IUnitOfWorkFactory.cs
│   └── ITransactionRunner.cs
├── SqlAccess/
│   ├── SqlConnectionFactory.cs
│   ├── SqlReadDb.cs
│   ├── SqlWriteDb.cs
│   ├── SqlUnitOfWork.cs
│   ├── SqlUnitOfWorkFactory.cs
│   ├── SqlTransactionRunner.cs
│   ├── NoteCard/
│   │   ├── Interfaces/
│   │   │   ├── ICollectionRepository.cs
│   │   │   ├── INotecardRepository.cs
│   │   │   └── IQuizRepository.cs
│   │   └── Repositories/
│   │       ├── CollectionRepository.cs
│   │       ├── NotecardRepository.cs
│   │       └── QuizRepository.cs
│   ├── Oscar/
│   │   ├── Interfaces/
│   │   │   └── IOscarRepository.cs
│   │   └── OscarRepository.cs
│   └── Budget/
│       ├── Interfaces/
│       │   ├── IBudgetRepository.cs
│       │   ├── IBudgetCollectionRepository.cs
│       │   ├── IBudgetItemRepository.cs
│       │   └── IRecurringItemRepository.cs
│       └── Repositories/
│           ├── BudgetRepository.cs
│           ├── BudgetCollectionRepository.cs
│           ├── BudgetItemRepository.cs
│           └── RecurringItemRepository.cs
└── Models/
    ├── NoteCard/
    │   ├── Collection/    (CollectionDto, CreateCollectionDto, UpdateCollectionDto)
    │   ├── Notecard/      (NotecardDto, CreateNotecardDto, UpdateNotecardDto, QuizNotecardDto)
    │   └── Quiz/          (QuizDto, CreateQuizDto, QuizInstanceDto, RecordQuizAnswerDto)
    ├── Oscar/             (CategoryDto, NomineeDto, LeaderboardEntryDto, SubmissionDto, UserResultDto, UpsertPickDto)
    └── Budget/
        ├── Budget/
        ├── BudgetItem/
        ├── Collection/
        └── RecurringItem/
```

## Core Abstractions

### `IConnectionFactory`
Creates `SqlConnection` instances from the `"Default"` connection string.

### `IReadDb`
Stateless read interface. Accepts a query string, parameters, and `CommandType` (default: `StoredProcedure`):

```csharp
Task<IEnumerable<T>> QueryAsync<T, U>(string queryString, U parameters, CommandType commandType);
Task<T?> QuerySingleOrDefaultAsync<T, U>(...);
Task<T> QuerySingleAsync<T, U>(...);
```

Backed by `SqlReadDb`, which uses Dapper against an open connection.

### `IWriteDb`
Same signature as `IReadDb` but operates within an active transaction. Backed by `SqlWriteDb`.

### `IUnitOfWork`
Holds an open `SqlConnection` and an active `SqlTransaction`. Exposes `Commit()` and `Rollback()`. Auto-rolls back on `Dispose` if not committed.

### `ITransactionRunner`
The primary way services execute write operations. Handles connection lifecycle, transaction commit/rollback automatically:

```csharp
await _transactionRunner.ExecuteAsync(async writeDb =>
{
    await _repo.CreateAsync(writeDb, dto);
});

var result = await _transactionRunner.ExecuteAsync(async writeDb =>
{
    return await _repo.CreateAsync(writeDb, dto);
});
```

On exception: rolls back and rethrows. On success: commits.

## Repository Pattern

Each repository accepts `IReadDb` (for queries) or `IWriteDb` (for mutations) as a method parameter — never injected into the constructor. This keeps repositories stateless and compatible with the transaction runner.

```csharp
// Read — injected IReadDb
Task<CollectionDto?> GetByIdAsync(IReadDb db, int id);

// Write — receives IWriteDb from ITransactionRunner
Task<int> CreateAsync(IWriteDb db, CreateCollectionDto dto);
```

Repositories are split by domain subdirectory:
- `SqlAccess/NoteCard/` — interfaces in `Interfaces/`, implementations in `Repositories/`
- `SqlAccess/Oscar/` — `IOscarRepository` + `OscarRepository` in the same directory
- `SqlAccess/Budget/` — same structure as NoteCard

## Stored Procedure Conventions

All queries call stored procedures by name. No inline SQL.

Naming: `{schema}.stp_{Verb}{Entity}`

Examples:
- `note.stp_CreateCollection`
- `note.stp_GetCollectionsByUser`
- `oscar.stp_UpsertPick`
- `budget.stp_GetBudgetsByCollection`

INSERT procedures return the new ID via a Dapper `OUTPUT` parameter (`@{Entity}Id`).

## Data Models (DTOs)

All DTOs are plain `record` or `class` types — no behavior.

| Suffix           | Purpose                        |
|------------------|--------------------------------|
| `*Dto`           | Data returned from DB queries  |
| `Create*Dto`     | Input for INSERT operations    |
| `Update*Dto`     | Input for UPDATE operations    |

Namespaces follow folder structure: `CookMartin.Data.Models.NoteCard.Collection`

## Dependency Injection (`DependencyInjection.cs`)

```csharp
services.AddScoped<IConnectionFactory, SqlConnectionFactory>();
services.AddScoped<IUnitOfWorkFactory, SqlUnitOfWorkFactory>();
services.AddScoped<IReadDb, SqlReadDb>();
services.AddScoped<ITransactionRunner, SqlTransactionRunner>();

// NoteCard
services.AddScoped<ICollectionRepository, CollectionRepository>();
services.AddScoped<INotecardRepository, NotecardRepository>();
services.AddScoped<IQuizRepository, QuizRepository>();

// Oscar
services.AddScoped<IOscarRepository, OscarRepository>();

// Budget (registered but not yet exposed via API endpoints)
services.AddScoped<IBudgetCollectionRepository, BudgetCollectionRepository>();
services.AddScoped<IBudgetRepository, BudgetRepository>();
services.AddScoped<IBudgetItemRepository, BudgetItemRepository>();
services.AddScoped<IRecurringItemRepository, RecurringItemRepository>();
```

Called from `CookMartin.API` as `services.AddDbService()`.

## Adding a New Feature

1. Create `Models/{Feature}/` with DTOs
2. Create `SqlAccess/{Feature}/Interfaces/I{Name}Repository.cs`
3. Create `SqlAccess/{Feature}/Repositories/{Name}Repository.cs` (or same folder if only one repo)
4. Register in `DependencyInjection.cs`

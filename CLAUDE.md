# CookMartin Backend — Solution Overview

## Project-Level CLAUDE.md Files

Each project has its own `CLAUDE.md` with folder structure, patterns, naming conventions, and key design decisions specific to that project. **Always read the relevant project's `CLAUDE.md` before searching its files** — it will often answer the question directly and save unnecessary codebase scanning.

| Project              | CLAUDE.md location                          |
| -------------------- | ------------------------------------------- |
| CookMartin.API       | `CookMartin.API/CLAUDE.md`                  |
| CookMartin.Budget    | `CookMartin.Budget/CLAUDE.md`               |
| CookMartin.NoteCard  | `CookMartin.NoteCard/CLAUDE.md`             |
| CookMartin.Oscar     | `CookMartin.Oscar/CLAUDE.md`                |
| CookMartin.Blob      | `CookMartin.Blob/CLAUDE.md`                 |
| CookMartin.Data      | `CookMartin.Data/CLAUDE.md`                 |
| CookMartin.Db        | `CookMartin.Db/CLAUDE.md`                   |

## Tech Stack

- **.NET 10 / C# 13**
- **ASP.NET Core Minimal APIs**
- **SQL Server (Azure SQL)** via Dapper 2.1 + stored procedures
- **Azure Blob Storage** via Azure SDK + DefaultAzureCredential
- **Microsoft Entra ID (Azure AD)** authentication via Microsoft.Identity.Web
- **SignalR** for real-time Oscar leaderboard updates
- **SSDT** database project for schema and stored procedures

## Projects and Dependency Graph

```
CookMartin.API          (Web host — entry point)
├── CookMartin.Blob     (Azure Blob storage + QR codes)
├── CookMartin.Budget   (Budget tracking business logic)
├── CookMartin.Data     (Dapper persistence layer)
├── CookMartin.NoteCard (Flashcard/quiz business logic)
└── CookMartin.Oscar    (Oscar ballot game logic)

CookMartin.Budget
└── CookMartin.Data

CookMartin.NoteCard
└── CookMartin.Data

CookMartin.Oscar
└── CookMartin.Data

CookMartin.Blob         (no project dependencies)
CookMartin.Data         (no project dependencies)
CookMartin.Db           (SSDT — SQL schema & stored procs, no C# code)
```

## Layered Architecture

```
HTTP Endpoints     (CookMartin.API)
      ↓
Services           (CookMartin.NoteCard, CookMartin.Oscar, CookMartin.Blob)
      ↓
Repositories       (CookMartin.Data — SqlAccess/)
      ↓
Stored Procedures  (CookMartin.Db)
```

## Feature Domains

| Domain   | Description                                              |
| -------- | -------------------------------------------------------- |
| NoteCard | Flashcard collections, study sessions, and quizzes       |
| Oscar    | Oscar ballot game with picks, winners, and leaderboard   |
| Budget   | Budget tracking — collections, budgets, items, recurring |
| Blob     | PDF upload/stream + optional QR code generation          |

## DI Registration Pattern

Each project exposes a single extension method on `IServiceCollection`:

```csharp
services.AddDbService();          // CookMartin.Data
services.AddNoteCardServices();   // CookMartin.NoteCard
services.AddOscarServices();      // CookMartin.Oscar
services.AddBudgetServices();     // CookMartin.Budget
services.AddBlobServices();       // CookMartin.Blob
```

All registered as `Scoped` unless noted otherwise.

## Authentication

- JWT Bearer via Microsoft Entra ID; config in `appsettings.json` under `AzureAd`
- Unauthenticated requests fall back to `userId = "guest"` via `ClaimsPrincipalExtensions.GetUserId()`
- NoteCard and Oscar both support guest access

## API Design Conventions

- All routes use Minimal API endpoint extension methods, grouped by domain
- Response envelope: `{ ok: bool, data?: T, error?: string, message?: string }`
- Error status codes: 409 Conflict (duplicate key), 403 Forbid, 404 NotFound, 400 BadRequest
- No inline SQL anywhere — all queries go through stored procedures

## Database Schema Ownership

Schemas map directly to domains: `note.*`, `oscar.*`, `budget.*`

Stored procedure naming: `{schema}.stp_{Verb}{Entity}` (e.g. `note.stp_CreateCollection`)

## Configuration Files

| File                           | Purpose                            |
| ------------------------------ | ---------------------------------- |
| `appsettings.json`             | Entra, Blob, CORS (prod origins)   |
| `appsettings.Development.json` | CORS localhost origins             |
| User Secrets                   | Connection string "Default" in dev |

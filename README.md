# CookMartin Home — Backend API

A personal multi-feature API built on **ASP.NET Core 10 Minimal APIs**, serving as the backbone for the CookMartin household's web projects. Deployed to **Azure App Service** with a **SQL Server** database and **Azure Blob Storage**, secured via **Microsoft Entra (Azure AD)**.

---

## Features

### NoteCard
A flashcard study tool. Users create collections of notecards and can run self-graded quizzes against them.

- Full CRUD for collections and notecards
- Quiz engine: generates a randomised question set from a collection, tracks answers, and calculates a final score
- User-scoped: collections and quizzes are isolated per authenticated user

### Oscar Ballot *(2026)*
A lightweight ballot app for friends to submit Oscar picks and compete on a leaderboard.

- Serves the full 98th Academy Award nominee list (24 categories) seeded at DB publish time
- Anonymous pick submission — no Entra account required, friends identify by a chosen username
- Real-time-friendly polling model: `GET /oscar/categories` and `GET /oscar/leaderboard` are designed for frequent polling
- Admin endpoints (Entra-protected) for marking and clearing winners as the ceremony unfolds

### Blob Storage
Manages publicly readable files in Azure Blob Storage.

- PDF upload with path control
- Resume hosting for John and Jacquie, with automatic **QR code generation** on upload

---

## Architecture

```
┌─────────────────────────────────────────────┐
│               CookMartin.API                │
│  Minimal API Endpoints  │  Azure AD Auth    │
└────────────┬────────────┴──────────┬────────┘
             │                       │
   ┌─────────▼──────────┐   ┌───────▼────────┐
   │  CookMartin.NoteCard│   │ CookMartin.Oscar│
   │  CookMartin.Blob    │   │   (Services)   │
   │     (Services)      │   └───────┬────────┘
   └─────────┬──────────┘           │
             └───────────┬──────────┘
                ┌────────▼────────┐
                │ CookMartin.Data │
                │  Repositories   │
                │    (Dapper)     │
                └────────┬────────┘
                         │
          ┌──────────────▼──────────────┐
          │        SQL Server           │
          │   Stored Procedures (SSDT)  │
          └─────────────────────────────┘
```

The architecture follows a strict layered pattern:

**Endpoints → Services → Repositories → Stored Procedures**

- **Endpoints** handle HTTP concerns (auth, request/response shaping, error catching)
- **Services** contain business logic and orchestrate transactions via `ITransactionRunner`
- **Repositories** execute parameterised stored procedure calls via Dapper
- **Stored Procedures** own all SQL — no inline queries in application code

Transactions are managed entirely in the service layer using `ITransactionRunner`, which auto-rolls back on any exception. Stored procedures are intentionally kept side-effect-only (no `BEGIN TRANSACTION`).

---

## Tech Stack

| Concern | Technology |
|---|---|
| Framework | ASP.NET Core 10 Minimal APIs |
| Language | C# 13, .NET 10 |
| Database | SQL Server (Azure SQL) |
| ORM | Dapper 2.1 |
| DB Schema Management | SSDT (SQL Server Data Tools) |
| Authentication | Microsoft Entra ID (Azure AD) via `Microsoft.Identity.Web` |
| File Storage | Azure Blob Storage |
| QR Codes | QRCoder |
| API Docs | Swagger / Swashbuckle + OpenAPI |
| Hosting | Azure App Service |
| Frontend Host | Azure Static Web Apps |

---

## Project Structure

```
Backend/
├── CookMartin.API/             # Entry point — endpoints, auth, middleware
│   ├── Endpoints/
│   │   ├── NoteCard/           # Collection, Notecard, Quiz endpoints
│   │   ├── Oscar/              # Ballot endpoints
│   │   └── BlobEndpoints.cs    # File upload endpoints
│   ├── Models/                 # Request DTOs
│   └── Extensions/             # ClaimsPrincipal helpers
│
├── CookMartin.NoteCard/        # NoteCard business logic
├── CookMartin.Oscar/           # Oscar ballot business logic
├── CookMartin.Blob/            # Azure Blob Storage service
│
├── CookMartin.Data/            # Data access layer (shared)
│   ├── Interfaces/             # IReadDb, IWriteDb, ITransactionRunner, etc.
│   ├── SqlAccess/              # Dapper implementations + repositories
│   └── Models/                 # Data transfer objects
│
└── CookMartin.Db/              # SQL Server Database Project (SSDT)
    ├── Security/               # Schema definitions (note, oscar)
    ├── Tables/                 # Table DDL
    ├── Programmability/        # Stored procedures
    │   ├── note/
    │   └── oscar/
    ├── Script.PostDeployment.sql
    └── seed_oscar_2026.sql     # 98th Academy Awards seed data
```

---

## API Reference

### NoteCard

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| `GET` | `/api/notecard/collections` | Entra | Get user's collections |
| `POST` | `/api/notecard/collections` | Entra | Create a collection |
| `PUT` | `/api/notecard/collections/{id}` | Entra | Update a collection |
| `DELETE` | `/api/notecard/collections/{id}` | Entra | Delete a collection |
| `GET` | `/api/notecard/collections/{id}/notecards` | Entra | Get notecards in a collection |
| `POST` | `/api/notecard/collections/{id}/notecards` | Entra | Add a notecard |
| `PUT` | `/api/notecard/notecards/{id}` | Entra | Update a notecard |
| `DELETE` | `/api/notecard/notecards/{id}` | Entra | Delete a notecard |
| `POST` | `/api/notecard/quizzes` | Entra | Start a quiz from a collection |
| `GET` | `/api/notecard/quizzes/{id}` | Entra | Get quiz details |
| `GET` | `/api/notecard/quizzes/{id}/instances` | Entra | Get all questions |
| `GET` | `/api/notecard/quizzes/{id}/next-notecard` | Entra | Get next unanswered question |
| `PUT` | `/api/notecard/quizzes/{id}/answers` | Entra | Record an answer |
| `DELETE` | `/api/notecard/quizzes/{id}` | Entra | Delete a quiz |

### Oscar Ballot

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| `GET` | `/oscar/categories` | None | All categories with nominees |
| `POST` | `/oscar/picks` | None | Submit a ballot `{ userName, picks: [nomineeId] }` |
| `GET` | `/oscar/submissions` | None | List of usernames who have submitted |
| `GET` | `/oscar/leaderboard` | None | Rankings by correct picks |
| `GET` | `/oscar/users/{userName}/results` | None | User's picks vs actual winners |
| `POST` | `/oscar/admin/winner/{nomineeId}` | Entra | Mark a winner |
| `DELETE` | `/oscar/admin/winner/{nomineeId}` | Entra | Clear a winner |

### Blob Storage

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| `POST` | `/api/blob/upload/pdf` | Entra | Upload any PDF to a given path |
| `POST` | `/api/blob/upload/john-resume` | Entra | Upload John's resume (generates QR code) |
| `POST` | `/api/blob/upload/jacquie-resume` | Entra | Upload Jacquie's resume (generates QR code) |

---

## Local Development

### Prerequisites
- .NET 10 SDK
- SQL Server (local or Azure SQL)
- Azure CLI (for Entra auth in development)

### Setup

1. **Clone and restore**
   ```bash
   git clone <repo>
   cd Backend
   dotnet restore
   ```

2. **Configure user secrets**
   ```bash
   cd CookMartin.API
   dotnet user-secrets set "ConnectionStrings:Default" "Server=...;Database=CookMartinDb;..."
   ```
   The `AzureAd` section in `appsettings.json` is pre-configured for the CookMartin Entra tenant.

3. **Deploy the database**
   Publish `CookMartin.Db/CookMartinDb.sqlproj` via Visual Studio's **Publish** feature or `SqlPackage`. The post-deployment script seeds the 2026 Oscar nominees automatically on first publish.

4. **Run**
   ```bash
   dotnet run --project CookMartin.API
   ```
   Swagger UI is available at `https://localhost:{port}/swagger` in Development.

---

## Deployment

The API is deployed to **Azure App Service** via GitHub Actions. The workflow authenticates to Azure using OIDC (no stored secrets) and publishes on push to `main`.

The database is deployed separately by publishing the SSDT project against the Azure SQL instance.

---

## Design Decisions

**Dapper over Entity Framework** — Stored procedures give explicit control over every query executed against the database. There are no surprise N+1 queries or implicit lazy loads. The Dapper `QueryAsync<T, U>` pattern keeps repositories lightweight and the SQL readable.

**SSDT for schema management** — The database schema lives in source control as `.sql` files, diffed and deployed like code. The post-deployment script pattern keeps seed data versioned alongside the schema that depends on it.

**Minimal APIs over Controllers** — Route handlers are co-located by feature, keeping the surface area of each endpoint visible at a glance without needing to navigate attribute-decorated controller classes.

**Feature projects** (`CookMartin.NoteCard`, `CookMartin.Oscar`) — Each feature owns its service interfaces and implementations in its own project. `CookMartin.Data` provides the shared persistence infrastructure (repositories, transaction runner, connection factory). This makes it straightforward to add new features without touching unrelated code.

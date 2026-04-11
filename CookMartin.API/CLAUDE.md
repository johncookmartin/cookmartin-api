# CookMartin.API

The HTTP host. Wires up authentication, DI, CORS, and Minimal API endpoints. Contains no business logic.

## Startup (`Program.cs`)

Registers services via extension methods from all feature projects, then maps endpoints:

```csharp
services.AddDbService();
services.AddNoteCardServices();
services.AddOscarServices();
services.AddBlobServices();

app.MapAllEndpoints();
app.MapHub<OscarHub>("/hubs/oscar");
```

## Folder Structure

```
CookMartin.API/
├── Endpoints/
│   ├── EndpointExtensions.cs        # MapAllEndpoints() aggregator
│   ├── BlobEndpoints.cs
│   ├── NoteCard/
│   │   ├── CollectionEndpoints.cs
│   │   ├── NotecardEndpoints.cs
│   │   └── QuizEndpoints.cs
│   └── Oscar/
│       └── OscarEndpoints.cs
├── Extensions/
│   └── ClaimsPrincipalExtensions.cs
├── Hubs/
│   └── OscarHub.cs
├── Models/
│   ├── NoteCard/                    # Request models (not DTOs)
│   └── Oscar/
└── Program.cs
```

## Endpoint Pattern

Each domain has a static class with a `Map*Endpoints(this WebApplication app)` extension method. All are aggregated in `EndpointExtensions.cs`:

```csharp
public static void MapAllEndpoints(this WebApplication app)
{
    app.MapBlobEndpoints();
    app.MapCollectionEndpoints();
    // ...
}
```

Within each file:
- Route groups: `app.MapGroup("/api/notecard/collections")`
- Auth: `.RequireAuthorization()` per endpoint or group; `.AllowAnonymous()` for public routes
- Keyed blob service injection: `[FromKeyedServices("public")] IBlobService blobService`

## Request Models vs DTOs

`Models/` contains API-layer request classes (e.g. `CreateCollectionRequest`). These are mapped to Data-layer DTOs (e.g. `CreateCollectionDto`) before being passed to services. Never pass request models directly to services.

Naming: `Create*Request`, `Update*Request`, `*Request`

## Authentication & User Identity

`ClaimsPrincipalExtensions.GetUserId()` resolves user identity in priority order:

1. `ClaimTypes.NameIdentifier`
2. `"oid"` claim (Azure AD object ID)
3. `"sub"` claim
4. Falls back to `"guest"` for unauthenticated requests

Always use this extension — never read claims directly in endpoints.

## Authorization Pattern in Endpoints

Endpoints that own resources verify ownership before returning or mutating:

```csharp
var userId = user.GetUserId();
if (collection.UserId != userId)
    return Results.Forbid();
```

## Error Handling

SQL duplicate key violations (SqlException numbers 2627 and 2601) are caught and returned as `Results.Conflict()`. All other exceptions return `Results.BadRequest()` with the exception message.

## SignalR (Oscar)

`OscarHub` is an empty hub class. Notifications are sent from endpoints via `IHubContext<OscarHub>`:

```csharp
await hubContext.Clients.All.SendAsync("LeaderboardChanged");
```

Events: `LeaderboardChanged`, `SubmissionsChanged`, `ResultsChanged`, `CategoriesChanged`

## Naming Conventions

| Thing              | Convention                              |
|--------------------|-----------------------------------------|
| Endpoint classes   | `{Domain}Endpoints` (static)            |
| Extension methods  | `Map{Domain}Endpoints`                  |
| Request models     | `Create*Request`, `Update*Request`      |
| Route prefix       | `/api/{domain}/{resource}` (auth)       |
| Public routes      | `/{domain}/{resource}` (no `/api` prefix) |
| Query params       | `[FromQuery]`                           |

## Configuration

```json
{
  "AzureAd": { "Instance", "TenantId", "ClientId" },
  "AzureBlob": { "Uri", "Blobs": ["public"] },
  "AllowedOrigins": ["https://..."],
  "ConnectionStrings": { "Default": "..." }
}
```

Connection string comes from User Secrets in development.

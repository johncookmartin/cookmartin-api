# CookMartin.Oscar

Business logic for the Oscar ballot game. Users submit picks for nominated films/performances; admins set winners; the service derives scores and a leaderboard.

## Folder Structure

```
CookMartin.Oscar/
├── DependencyInjection.cs
└── Services/
    ├── Interfaces/
    │   └── IOscarService.cs
    └── OscarService.cs
```

## Service Interface (`IOscarService`)

```csharp
Task<IEnumerable<CategoryDto>> GetCategoriesWithNomineesAsync();
Task SubmitPicksAsync(string userName, IEnumerable<int> nomineeIds);
Task SetWinnerAsync(int nomineeId);
Task ClearWinnerAsync(int nomineeId);
Task<IEnumerable<LeaderboardEntryDto>> GetLeaderboardAsync();
Task<IEnumerable<UserResultDto>> GetUserResultsAsync(string userName);
Task<IEnumerable<SubmissionDto>> GetSubmissionsAsync();
```

## Implementation Notes

**`SubmitPicksAsync`** loops over `nomineeIds` and calls `IOscarRepository.UpsertPickAsync` (backed by `oscar.stp_UpsertPick`) for each. The upsert pattern allows resubmission — a user can change their picks.

**Read methods** delegate directly to `IOscarRepository` with no additional logic.

**`GetCategoriesWithNomineesAsync`** — the repository returns flat rows from the SP; `OscarRepository` groups them into `CategoryDto` objects each containing a `List<NomineeDto>`.

**Mutations use `ITransactionRunner`:**

```csharp
await _transactionRunner.ExecuteAsync(async writeDb =>
{
    foreach (var nomineeId in nomineeIds)
        await _oscarRepository.UpsertPickAsync(writeDb, userName, nomineeId);
});
```

## Guest Access

Oscar supports anonymous submissions — the `userName` is provided by the caller (not derived from a JWT claim). Callers are responsible for validating or tracking the username.

## Dependencies

- `CookMartin.Data` — `IOscarRepository`, `IReadDb`, `ITransactionRunner`, and Oscar DTOs

## Dependency Injection (`DependencyInjection.cs`)

```csharp
services.AddScoped<IOscarService, OscarService>();
```

Called from `CookMartin.API` as `services.AddOscarServices()`.

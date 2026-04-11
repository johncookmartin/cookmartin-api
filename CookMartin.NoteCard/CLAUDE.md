# CookMartin.NoteCard

Business logic for the flashcard study feature. Sits between the API endpoints and the data repositories. Contains no SQL or HTTP concerns.

## Folder Structure

```
CookMartin.NoteCard/
├── DependencyInjection.cs
└── Services/
    ├── Interfaces/
    │   ├── ICollectionService.cs
    │   ├── INotecardService.cs
    │   └── IQuizService.cs
    └── CollectionService.cs
    └── NotecardService.cs
    └── QuizService.cs
```

## Services

### `ICollectionService` / `CollectionService`

CRUD for note collections.

Notable: `CreateGuestCollectionAsync()` auto-seeds a "Default Guest Collection" for the guest user via `note.stp_SeedGuest`. Called on first guest access — idempotent at the SP level.

### `INotecardService` / `NotecardService`

CRUD for individual notecards within a collection, plus quiz navigation.

`GetNextNoteCardAsync(int quizId)` — returns a random unanswered card from the quiz. Uses a private `Random _rng` to shuffle the list of `QuizNotecardDto` instances before picking.

### `IQuizService` / `QuizService`

Manages quiz lifecycle: create, record answers, detect completion.

`RecordAnswerAsync(RecordQuizAnswerDto)`:
- Records the answer via `note.stp_RecordQuizAnswer`
- Fetches all instances for the quiz
- If all instances are answered, calls `note.stp_CompleteQuiz`
- Returns `true` when the quiz is complete

## Patterns

**Write operations always go through `ITransactionRunner`:**

```csharp
return await _transactionRunner.ExecuteAsync(async writeDb =>
{
    return await _collectionRepository.CreateAsync(writeDb, dto);
});
```

**Read operations call the repository directly with the injected `IReadDb`:**

```csharp
return await _collectionRepository.GetByIdAsync(_readDb, id);
```

## Naming Conventions

| Thing             | Convention                              |
|-------------------|-----------------------------------------|
| Service classes   | `{Domain}Service`                       |
| Interfaces        | `I{Domain}Service`                      |
| All methods       | `*Async` suffix                         |
| Single-item reads | Return `T?` (nullable)                  |
| Collection reads  | Return `IEnumerable<T>`                 |
| Mutations         | Return the created/affected entity or `bool` |

## Dependencies

- `CookMartin.Data` — repositories (`ICollectionRepository`, `INotecardRepository`, `IQuizRepository`), `IReadDb`, `ITransactionRunner`, and all DTOs

## Dependency Injection (`DependencyInjection.cs`)

```csharp
services.AddScoped<ICollectionService, CollectionService>();
services.AddScoped<INotecardService, NotecardService>();
services.AddScoped<IQuizService, QuizService>();
```

Called from `CookMartin.API` as `services.AddNoteCardServices()`.

# CookMartin.Budget

Business logic for the budget tracking feature. Sits between the API endpoints and the data repositories. Contains no SQL or HTTP concerns.

## Folder Structure

```
CookMartin.Budget/
├── DependencyInjection.cs
└── Services/
    ├── Interfaces/
    │   ├── IBudgetCollectionService.cs
    │   ├── IBudgetService.cs
    │   ├── IBudgetItemService.cs
    │   └── IRecurringItemService.cs
    ├── BudgetCollectionService.cs
    ├── BudgetService.cs
    ├── BudgetItemService.cs
    └── RecurringItemService.cs
```

## Services

### `IBudgetCollectionService` / `BudgetCollectionService`

CRUD for budget collections, plus member management (`AddUserAsync` / `RemoveUserAsync`).

A collection has one owner (`OwnerId`) and zero or more shared members (`CollectionUsers`). Both owner and members can view and create child records; only the owner can update or delete the collection or remove members.

### `IBudgetService` / `BudgetService`

CRUD for budgets within a collection. Budgets belong to a collection and are accessible to all collection members.

### `IBudgetItemService` / `BudgetItemService`

CRUD for individual budget line items within a budget. Each item records who created it (`UserId`) but authorization is enforced at the collection level.

### `IRecurringItemService` / `RecurringItemService`

CRUD for recurring income/expense templates within a collection. `IsShared` indicates whether the item applies to the whole household or just the creating user.

## Patterns

**Write operations always go through `ITransactionRunner`:**

```csharp
return await _transactionRunner.ExecuteAsync(async writeDb =>
    await _repository.CreateAsync(writeDb, dto));
```

**Read operations call the repository directly (no transaction needed):**

```csharp
return await _repository.GetByIdAsync(id);
```

**Create methods return the newly created entity** (create → fetch → return):

```csharp
var id = await _transactionRunner.ExecuteAsync(...);
var entity = await _repository.GetByIdAsync(id);
return entity ?? throw new InvalidOperationException("Failed to create ...");
```

## Naming Conventions

| Thing             | Convention                              |
|-------------------|-----------------------------------------|
| Service classes   | `{Domain}Service`                       |
| Interfaces        | `I{Domain}Service`                      |
| All methods       | `*Async` suffix                         |
| Single-item reads | Return `T?` (nullable)                  |
| Collection reads  | Return `IEnumerable<T>`                 |
| Mutations         | Return created entity or `bool`         |

## Dependencies

- `CookMartin.Data` — repositories (`IBudgetCollectionRepository`, `IBudgetRepository`, `IBudgetItemRepository`, `IRecurringItemRepository`), `ITransactionRunner`, and all DTOs under `CookMartin.Data.Models.Budget.*`

## Data Models (from CookMartin.Data)

| DTO                      | Namespace                                    |
|--------------------------|----------------------------------------------|
| `BudgetCollectionDto`    | `CookMartin.Data.Models.Budget.Collection`   |
| `BudgetCollectionUserDto`| `CookMartin.Data.Models.Budget.Collection`   |
| `CreateBudgetCollectionDto` | `CookMartin.Data.Models.Budget.Collection` |
| `UpdateBudgetCollectionDto` | `CookMartin.Data.Models.Budget.Collection` |
| `BudgetDto`              | `CookMartin.Data.Models.Budget.Budget`       |
| `CreateBudgetDto`        | `CookMartin.Data.Models.Budget.Budget`       |
| `UpdateBudgetDto`        | `CookMartin.Data.Models.Budget.Budget`       |
| `BudgetItemDto`          | `CookMartin.Data.Models.Budget.BudgetItem`   |
| `CreateBudgetItemDto`    | `CookMartin.Data.Models.Budget.BudgetItem`   |
| `UpdateBudgetItemDto`    | `CookMartin.Data.Models.Budget.BudgetItem`   |
| `RecurringItemDto`       | `CookMartin.Data.Models.Budget.RecurringItem`|
| `CreateRecurringItemDto` | `CookMartin.Data.Models.Budget.RecurringItem`|
| `UpdateRecurringItemDto` | `CookMartin.Data.Models.Budget.RecurringItem`|

## Dependency Injection (`DependencyInjection.cs`)

```csharp
services.AddScoped<IBudgetCollectionService, BudgetCollectionService>();
services.AddScoped<IBudgetService, BudgetService>();
services.AddScoped<IBudgetItemService, BudgetItemService>();
services.AddScoped<IRecurringItemService, RecurringItemService>();
```

Called from `CookMartin.API` as `services.AddBudgetServices()`.

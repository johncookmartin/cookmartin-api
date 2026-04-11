using CookMartin.API.Extensions;
using CookMartin.API.Models.Budget;
using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Models.Budget.BudgetItem;
using System.Security.Claims;

namespace CookMartin.API.Endpoints.Budget;

public static class BudgetItemEndpoints
{
    public static void MapBudgetItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/budget")
            .RequireAuthorization()
            .WithTags("Budget - Items");

        group.MapGet("budgets/{budgetId:int}/items", async (
            int budgetId,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService,
            IBudgetItemService budgetItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var budget = await budgetService.GetByIdAsync(budgetId);

                if (budget == null)
                    return Results.NotFound(new { ok = false, error = "Budget not found" });

                var collection = await collectionService.GetByIdAsync(budget.CollectionId);

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var items = await budgetItemService.GetByBudgetAsync(budgetId);
                return Results.Ok(new { ok = true, data = items });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetBudgetItemsByBudget");

        group.MapPost("budgets/{budgetId:int}/items", async (
            int budgetId,
            CreateBudgetItemRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService,
            IBudgetItemService budgetItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var budget = await budgetService.GetByIdAsync(budgetId);

                if (budget == null)
                    return Results.NotFound(new { ok = false, error = "Budget not found" });

                var collection = await collectionService.GetByIdAsync(budget.CollectionId);

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var dto = new CreateBudgetItemDto
                {
                    BudgetId       = budgetId,
                    UserId         = userId,
                    Label          = request.Label,
                    BudgetedAmount = request.BudgetedAmount,
                    DueDate        = request.DueDate,
                    Type           = request.Type
                };

                var item = await budgetItemService.CreateAsync(dto);
                return Results.Ok(new { ok = true, data = item });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("CreateBudgetItem");

        group.MapGet("items/{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService,
            IBudgetItemService budgetItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var item = await budgetItemService.GetByIdAsync(id);

                if (item == null)
                    return Results.NotFound(new { ok = false, error = "Budget item not found" });

                var budget = await budgetService.GetByIdAsync(item.BudgetId);
                var collection = budget != null ? await collectionService.GetByIdAsync(budget.CollectionId) : null;

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                return Results.Ok(new { ok = true, data = item });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetBudgetItemById");

        group.MapPut("items/{id:int}", async (
            int id,
            UpdateBudgetItemRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService,
            IBudgetItemService budgetItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var item = await budgetItemService.GetByIdAsync(id);

                if (item == null)
                    return Results.NotFound(new { ok = false, error = "Budget item not found" });

                var budget = await budgetService.GetByIdAsync(item.BudgetId);
                var collection = budget != null ? await collectionService.GetByIdAsync(budget.CollectionId) : null;

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var dto = new UpdateBudgetItemDto
                {
                    BudgetItemId   = id,
                    Label          = request.Label,
                    BudgetedAmount = request.BudgetedAmount,
                    DueDate        = request.DueDate,
                    ActualAmount   = request.ActualAmount,
                    ActualDate     = request.ActualDate,
                    Type           = request.Type
                };

                var success = await budgetItemService.UpdateAsync(dto);

                if (success)
                {
                    var updated = await budgetItemService.GetByIdAsync(id);
                    return Results.Ok(new { ok = true, data = updated });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to update budget item" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("UpdateBudgetItem");

        group.MapDelete("items/{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService,
            IBudgetItemService budgetItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var item = await budgetItemService.GetByIdAsync(id);

                if (item == null)
                    return Results.NotFound(new { ok = false, error = "Budget item not found" });

                var budget = await budgetService.GetByIdAsync(item.BudgetId);
                var collection = budget != null ? await collectionService.GetByIdAsync(budget.CollectionId) : null;

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var success = await budgetItemService.DeleteAsync(id);

                if (success)
                    return Results.Ok(new { ok = true, message = "Budget item deleted successfully" });

                return Results.BadRequest(new { ok = false, error = "Failed to delete budget item" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("DeleteBudgetItem");
    }

    private static bool IsCollectionMember(Data.Models.Budget.Collection.BudgetCollectionDto collection, string userId) =>
        collection.OwnerId == userId || collection.Users.Any(u => u.UserId == userId);
}

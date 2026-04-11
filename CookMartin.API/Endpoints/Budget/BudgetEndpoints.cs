using CookMartin.API.Extensions;
using CookMartin.API.Models.Budget;
using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Models.Budget.Budget;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CookMartin.API.Endpoints.Budget;

public static class BudgetEndpoints
{
    public static void MapBudgetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/budget")
            .RequireAuthorization()
            .WithTags("Budget - Budgets");

        group.MapGet("collections/{collectionId:int}/budgets", async (
            int collectionId,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(collectionId);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (!IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var budgets = await budgetService.GetByCollectionAsync(collectionId);
                return Results.Ok(new { ok = true, data = budgets });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetBudgetsByCollection");

        group.MapPost("collections/{collectionId:int}/budgets", async (
            int collectionId,
            CreateBudgetRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(collectionId);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (!IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var dto = new CreateBudgetDto
                {
                    CollectionId   = collectionId,
                    Name           = request.Name,
                    Type           = request.Type,
                    StartingAmount = request.StartingAmount
                };

                var budget = await budgetService.CreateAsync(dto);
                return Results.Ok(new { ok = true, data = budget });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Results.Conflict(new { ok = false, error = "A budget with the same name already exists in this collection." });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("CreateBudget");

        group.MapGet("budgets/{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var budget = await budgetService.GetByIdAsync(id);

                if (budget == null)
                    return Results.NotFound(new { ok = false, error = "Budget not found" });

                var collection = await collectionService.GetByIdAsync(budget.CollectionId);

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                return Results.Ok(new { ok = true, data = budget });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetBudgetById");

        group.MapPut("budgets/{id:int}", async (
            int id,
            UpdateBudgetRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var budget = await budgetService.GetByIdAsync(id);

                if (budget == null)
                    return Results.NotFound(new { ok = false, error = "Budget not found" });

                var collection = await collectionService.GetByIdAsync(budget.CollectionId);

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var dto = new UpdateBudgetDto
                {
                    BudgetId       = id,
                    Name           = request.Name,
                    Type           = request.Type,
                    StartingAmount = request.StartingAmount
                };

                var success = await budgetService.UpdateAsync(dto);

                if (success)
                {
                    var updated = await budgetService.GetByIdAsync(id);
                    return Results.Ok(new { ok = true, data = updated });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to update budget" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("UpdateBudget");

        group.MapDelete("budgets/{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IBudgetService budgetService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var budget = await budgetService.GetByIdAsync(id);

                if (budget == null)
                    return Results.NotFound(new { ok = false, error = "Budget not found" });

                var collection = await collectionService.GetByIdAsync(budget.CollectionId);

                if (collection == null || collection.OwnerId != userId)
                    return Results.Forbid();

                var success = await budgetService.DeleteAsync(id);

                if (success)
                    return Results.Ok(new { ok = true, message = "Budget deleted successfully" });

                return Results.BadRequest(new { ok = false, error = "Failed to delete budget" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("DeleteBudget");
    }

    private static bool IsCollectionMember(Data.Models.Budget.Collection.BudgetCollectionDto collection, string userId) =>
        collection.OwnerId == userId || collection.Users.Any(u => u.UserId == userId);
}

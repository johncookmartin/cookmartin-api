using CookMartin.API.Extensions;
using CookMartin.API.Models.Budget;
using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Models.Budget.RecurringItem;
using System.Security.Claims;

namespace CookMartin.API.Endpoints.Budget;

public static class RecurringItemEndpoints
{
    public static void MapRecurringItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/budget")
            .RequireAuthorization()
            .WithTags("Budget - Recurring Items");

        group.MapGet("collections/{collectionId:int}/recurring", async (
            int collectionId,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IRecurringItemService recurringItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(collectionId);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (!IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var items = await recurringItemService.GetByCollectionAsync(collectionId);
                return Results.Ok(new { ok = true, data = items });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetRecurringItemsByCollection");

        group.MapPost("collections/{collectionId:int}/recurring", async (
            int collectionId,
            CreateRecurringItemRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IRecurringItemService recurringItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(collectionId);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (!IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var dto = new CreateRecurringItemDto
                {
                    CollectionId = collectionId,
                    UserId       = userId,
                    Label        = request.Label,
                    Amount       = request.Amount,
                    Type         = request.Type,
                    IsShared     = request.IsShared
                };

                var item = await recurringItemService.CreateAsync(dto);
                return Results.Ok(new { ok = true, data = item });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("CreateRecurringItem");

        group.MapGet("recurring/{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IRecurringItemService recurringItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var item = await recurringItemService.GetByIdAsync(id);

                if (item == null)
                    return Results.NotFound(new { ok = false, error = "Recurring item not found" });

                var collection = await collectionService.GetByIdAsync(item.CollectionId);

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                return Results.Ok(new { ok = true, data = item });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetRecurringItemById");

        group.MapPut("recurring/{id:int}", async (
            int id,
            UpdateRecurringItemRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IRecurringItemService recurringItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var item = await recurringItemService.GetByIdAsync(id);

                if (item == null)
                    return Results.NotFound(new { ok = false, error = "Recurring item not found" });

                var collection = await collectionService.GetByIdAsync(item.CollectionId);

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var dto = new UpdateRecurringItemDto
                {
                    RecurringItemId = id,
                    Label           = request.Label,
                    Amount          = request.Amount,
                    Type            = request.Type,
                    IsShared        = request.IsShared
                };

                var success = await recurringItemService.UpdateAsync(dto);

                if (success)
                {
                    var updated = await recurringItemService.GetByIdAsync(id);
                    return Results.Ok(new { ok = true, data = updated });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to update recurring item" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("UpdateRecurringItem");

        group.MapDelete("recurring/{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService,
            IRecurringItemService recurringItemService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var item = await recurringItemService.GetByIdAsync(id);

                if (item == null)
                    return Results.NotFound(new { ok = false, error = "Recurring item not found" });

                var collection = await collectionService.GetByIdAsync(item.CollectionId);

                if (collection == null || !IsCollectionMember(collection, userId))
                    return Results.Forbid();

                var success = await recurringItemService.DeleteAsync(id);

                if (success)
                    return Results.Ok(new { ok = true, message = "Recurring item deleted successfully" });

                return Results.BadRequest(new { ok = false, error = "Failed to delete recurring item" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("DeleteRecurringItem");
    }

    private static bool IsCollectionMember(Data.Models.Budget.Collection.BudgetCollectionDto collection, string userId) =>
        collection.OwnerId == userId || collection.Users.Any(u => u.UserId == userId);
}

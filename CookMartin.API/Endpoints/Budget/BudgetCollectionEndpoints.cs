using CookMartin.API.Extensions;
using CookMartin.API.Models.Budget;
using CookMartin.Budget.Services.Interfaces;
using CookMartin.Data.Models.Budget.Collection;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CookMartin.API.Endpoints.Budget;

public static class BudgetCollectionEndpoints
{
    public static void MapBudgetCollectionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/budget/collections")
            .RequireAuthorization()
            .WithTags("Budget - Collections");

        group.MapPost("", async (
            CreateBudgetCollectionRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            var dto = new CreateBudgetCollectionDto
            {
                OwnerId       = userId,
                Name          = request.Name,
                EmergencyFund = request.EmergencyFund
            };

            try
            {
                var collection = await collectionService.CreateAsync(dto);
                return Results.Ok(new { ok = true, data = collection });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Results.Conflict(new { ok = false, error = "A collection with the same name already exists." });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("CreateBudgetCollection");

        group.MapGet("", async (
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collections = await collectionService.GetByUserAsync(userId);
                return Results.Ok(new { ok = true, data = collections });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetBudgetCollectionsByUser");

        group.MapGet("{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(id);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (!IsAuthorized(collection, userId))
                    return Results.Forbid();

                return Results.Ok(new { ok = true, data = collection });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetBudgetCollectionById");

        group.MapPut("{id:int}", async (
            int id,
            UpdateBudgetCollectionRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(id);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (collection.OwnerId != userId)
                    return Results.Forbid();

                var dto = new UpdateBudgetCollectionDto
                {
                    CollectionId  = id,
                    Name          = request.Name,
                    EmergencyFund = request.EmergencyFund
                };

                var success = await collectionService.UpdateAsync(dto);

                if (success)
                {
                    var updated = await collectionService.GetByIdAsync(id);
                    return Results.Ok(new { ok = true, data = updated });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to update collection" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("UpdateBudgetCollection");

        group.MapDelete("{id:int}", async (
            int id,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(id);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (collection.OwnerId != userId)
                    return Results.Forbid();

                var success = await collectionService.DeleteAsync(id);

                if (success)
                    return Results.Ok(new { ok = true, message = "Collection deleted successfully" });

                return Results.BadRequest(new { ok = false, error = "Failed to delete collection" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("DeleteBudgetCollection");

        group.MapPost("{id:int}/users", async (
            int id,
            AddCollectionUserRequest request,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(id);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (collection.OwnerId != userId)
                    return Results.Forbid();

                var collectionUserId = await collectionService.AddUserAsync(id, request.UserId);
                return Results.Ok(new { ok = true, data = new { collectionUserId } });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Results.Conflict(new { ok = false, error = "User is already a member of this collection." });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("AddBudgetCollectionUser");

        group.MapDelete("{id:int}/users/{memberId}", async (
            int id,
            string memberId,
            ClaimsPrincipal user,
            IBudgetCollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var collection = await collectionService.GetByIdAsync(id);

                if (collection == null)
                    return Results.NotFound(new { ok = false, error = "Collection not found" });

                if (collection.OwnerId != userId)
                    return Results.Forbid();

                var success = await collectionService.RemoveUserAsync(id, memberId);

                if (success)
                    return Results.Ok(new { ok = true, message = "User removed from collection" });

                return Results.NotFound(new { ok = false, error = "User is not a member of this collection" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("RemoveBudgetCollectionUser");
    }

    private static bool IsAuthorized(BudgetCollectionDto collection, string userId) =>
        collection.OwnerId == userId || collection.Users.Any(u => u.UserId == userId);
}

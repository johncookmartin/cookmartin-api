using CookMartin.API.Models.NoteCard;
using CookMartin.Data.Models.NoteCard.Collection;
using CookMartin.Data.Models.NoteCard.Notecard;
using CookMartin.NoteCard.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CookMartin.API.Endpoints.NoteCard;

public static class CollectionEndpoints
{
    public static void MapCollectionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/notecard/collections")
            .AllowAnonymous()
            .WithTags("NoteCard - Collections");

        group.MapPost("", async (
            CreateCollectionRequest request,
            ClaimsPrincipal user,
            ICollectionService collectionService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("oid")?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "guest";

            CreateCollectionDto dto = new() { UserId = userId, Name = request.Name };

            try
            {
                var collection = await collectionService.CreateCollectionAsync(dto);
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
        .WithName("CreateCollection");

        group.MapGet("", async (
            ClaimsPrincipal user,
            ICollectionService collectionService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("oid")?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "guest";

            try
            {
                var collections = await collectionService.GetCollectionsByUserAsync(userId);
                return Results.Ok(new { ok = true, data = collections });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetUserCollections");

        group.MapGet("{id:int}", async (
            int id,
            ClaimsPrincipal user,
            ICollectionService collectionService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("oid")?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "guest";

            try
            {
                var collection = await collectionService.GetCollectionByIdAsync(id);

                if (collection == null)
                {
                    return Results.NotFound(new { ok = false, error = "Collection not found" });
                }

                if (collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                return Results.Ok(new { ok = true, data = collection });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetCollectionById");

        group.MapPut("{id:int}", async (
            int id,
            UpdateCollectionDto dto,
            ClaimsPrincipal user,
            ICollectionService collectionService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("oid")?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "guest";

            try
            {
                var collection = await collectionService.GetCollectionByIdAsync(id);

                if (collection == null)
                {
                    return Results.NotFound(new { ok = false, error = "Collection not found" });
                }

                if (collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                dto.CollectionId = id;
                var success = await collectionService.UpdateCollectionAsync(id, dto);

                if (success)
                {
                    var updatedCollection = await collectionService.GetCollectionByIdAsync(id);
                    return Results.Ok(new { ok = true, data = updatedCollection });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to update collection" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("UpdateCollection");

        group.MapDelete("{id:int}", async (
            int id,
            ClaimsPrincipal user,
            ICollectionService collectionService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("oid")?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "guest";

            try
            {
                var collection = await collectionService.GetCollectionByIdAsync(id);

                if (collection == null)
                {
                    return Results.NotFound(new { ok = false, error = "Collection not found" });
                }

                if (collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                var success = await collectionService.DeleteCollectionAsync(id);

                if (success)
                {
                    return Results.Ok(new { ok = true, message = "Collection deleted successfully" });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to delete collection" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("DeleteCollection");

        group.MapGet("{id:int}/notecards", async (
            int id,
            ClaimsPrincipal user,
            INotecardService notecardService,
            ICollectionService collectionService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("oid")?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "guest";

            try
            {
                var collection = await collectionService.GetCollectionByIdAsync(id);

                if (collection == null)
                {
                    return Results.NotFound(new { ok = false, error = "Collection not found" });
                }

                if (collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                var notecards = await notecardService.GetNotecardsByCollectionAsync(id);
                return Results.Ok(new { ok = true, data = notecards });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetNotecardsByCollection");

        group.MapPost("{id:int}/notecards", async (
            int id,
            CreateNotecardRequest request,
            ClaimsPrincipal user,
            INotecardService notecardService,
            ICollectionService collectionService) =>
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst("oid")?.Value
                ?? user.FindFirst("sub")?.Value
                ?? "guest";

            try
            {
                var collection = await collectionService.GetCollectionByIdAsync(id);

                if (collection == null)
                {
                    return Results.NotFound(new { ok = false, error = "Collection not found" });
                }

                if (collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                CreateNotecardDto dto = new()
                {
                    FrontDescription = request.FrontDescription,
                    BackDescription = request.BackDescription,
                    CollectionId = id
                };
                var notecard = await notecardService.CreateNotecardAsync(dto);
                return Results.Ok(new { ok = true, data = notecard });
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                return Results.Conflict(new { ok = false, error = "A notecard with the same description already exists in this collection." });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("CreateNotecard");
    }
}

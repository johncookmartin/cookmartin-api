using CookMartin.API.Extensions;
using CookMartin.Data.Models.NoteCard.Notecard;
using CookMartin.NoteCard.Services.Interfaces;
using System.Security.Claims;

namespace CookMartin.API.Endpoints.NoteCard;

public static class NotecardEndpoints
{
    public static void MapNotecardEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/notecard/notecards")
            .AllowAnonymous()
            .WithTags("NoteCard - Notecards");

        group.MapGet("{id:int}", async (
            int id,
            ClaimsPrincipal user,
            INotecardService notecardService,
            ICollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var notecard = await notecardService.GetNotecardByIdAsync(id);

                if (notecard == null)
                {
                    return Results.NotFound(new { ok = false, error = "Notecard not found" });
                }

                var collection = await collectionService.GetCollectionByIdAsync(notecard.CollectionId);

                if (collection == null || collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                return Results.Ok(new { ok = true, data = notecard });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetNotecardById");

        group.MapPut("{id:int}", async (
            int id,
            UpdateNotecardDto dto,
            ClaimsPrincipal user,
            INotecardService notecardService,
            ICollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var notecard = await notecardService.GetNotecardByIdAsync(id);

                if (notecard == null)
                {
                    return Results.NotFound(new { ok = false, error = "Notecard not found" });
                }

                var collection = await collectionService.GetCollectionByIdAsync(notecard.CollectionId);

                if (collection == null || collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                dto.NotecardId = id;
                var success = await notecardService.UpdateNotecardAsync(id, dto);

                if (success)
                {
                    var updatedNotecard = await notecardService.GetNotecardByIdAsync(id);
                    return Results.Ok(new { ok = true, data = updatedNotecard });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to update notecard" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("UpdateNotecard");

        group.MapDelete("{id:int}", async (
            int id,
            ClaimsPrincipal user,
            INotecardService notecardService,
            ICollectionService collectionService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var notecard = await notecardService.GetNotecardByIdAsync(id);

                if (notecard == null)
                {
                    return Results.NotFound(new { ok = false, error = "Notecard not found" });
                }

                var collection = await collectionService.GetCollectionByIdAsync(notecard.CollectionId);

                if (collection == null || collection.UserId != userId)
                {
                    return Results.Forbid();
                }

                var success = await notecardService.DeleteNotecardAsync(id);

                if (success)
                {
                    return Results.Ok(new { ok = true, message = "Notecard deleted successfully" });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to delete notecard" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("DeleteNotecard");
    }
}

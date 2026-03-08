using CookMartin.API.Extensions;
using CookMartin.API.Models.NoteCard;
using CookMartin.Data.Models.NoteCard.Quiz;
using CookMartin.NoteCard.Services;
using CookMartin.NoteCard.Services.Interfaces;
using System.Security.Claims;

namespace CookMartin.API.Endpoints.NoteCard;

public static class QuizEndpoints
{
    public static void MapQuizEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/notecard/quizzes")
            .AllowAnonymous()
            .WithTags("NoteCard - Quizzes");

        group.MapPost("", async (
            CreateQuizRequest request,
            ClaimsPrincipal user,
            IQuizService quizService) =>
        {
            var userId = user.GetUserId();

            CreateQuizDto dto = new() { CollectionId = request.CollectionId, UserId = userId };

            try
            {
                var collection = await quizService.CreateQuizAsync(dto);
                return Results.Ok(new { ok = true, data = collection });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("CreateQuiz");

        group.MapPut("{quizId:int}/instance/{instanceId:int}", async (
            int quizId,
            int instanceId,
            RecordQuizAnswerRequest request,
            ClaimsPrincipal user,
            IQuizService quizService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var quiz = await quizService.GetQuizByIdAsync(quizId);

                if (quiz == null)
                {
                    return Results.NotFound(new { ok = false, error = "Quiz not found" });
                }

                if (quiz.UserId != userId)
                {
                    return Results.Forbid();
                }

                RecordQuizAnswerDto dto = new() { QuizId = quizId, InstanceId = instanceId, IsCorrect = request.IsCorrect };

                var result = await quizService.RecordAnswerAsnyc(dto);
                return Results.Ok(new { ok = true, isComplete = result });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("RecordQuizAnswer");

        group.MapGet("{quizId:int}/notecard", async (
            int quizId,
            ClaimsPrincipal user,
            IQuizService quizService,
            INotecardService notecardService
            ) =>
        {
            var userId = user.GetUserId();

            try
            {
                var quiz = await quizService.GetQuizByIdAsync(quizId);
                if (quiz == null)
                {
                    return Results.NotFound(new { ok = false, error = "Quiz not found" });
                }
                if (quiz.UserId != userId)
                {
                    return Results.Forbid();
                }
                var notecard = await notecardService.GetNextNoteCardAsync(quizId);
                return Results.Ok(new { ok = true, data = notecard });

            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetQuizNotecard");

        group.MapGet("", async (
            ClaimsPrincipal user,
            IQuizService quizService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var quizzes = await quizService.GetQuizzesByUserAsync(userId);
                return Results.Ok(new { ok = true, data = quizzes });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetUserQuizzes");

        group.MapGet("{quizId:int}", async (
            int quizId,
            ClaimsPrincipal user,
            IQuizService quizService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var quiz = await quizService.GetQuizByIdAsync(quizId);

                if (quiz == null)
                {
                    return Results.NotFound(new { ok = false, error = "Quiz not found" });
                }

                if (quiz.UserId != userId)
                {
                    return Results.Forbid();
                }

                return Results.Ok(new { ok = true, data = quiz });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetQuizById");

        group.MapGet("{quizId:int}/instances", async (
            int quizId,
            ClaimsPrincipal user,
            IQuizService quizService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var quiz = await quizService.GetQuizByIdAsync(quizId);

                if (quiz == null)
                {
                    return Results.NotFound(new { ok = false, error = "Quiz not found" });
                }

                if (quiz.UserId != userId)
                {
                    return Results.Forbid();
                }

                var instances = await quizService.GetInstancesByIdAsync(quizId);
                return Results.Ok(new { ok = true, data = instances });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetQuizInstances");

        group.MapDelete("{quizId:int}", async (
            int quizId,
            ClaimsPrincipal user,
            IQuizService quizService) =>
        {
            var userId = user.GetUserId();

            try
            {
                var quiz = await quizService.GetQuizByIdAsync(quizId);

                if (quiz == null)
                {
                    return Results.NotFound(new { ok = false, error = "Quiz not found" });
                }

                if (quiz.UserId != userId)
                {
                    return Results.Forbid();
                }

                var success = await quizService.DeleteQuizAsync(quizId);

                if (success)
                {
                    return Results.Ok(new { ok = true, message = "Quiz deleted successfully" });
                }

                return Results.BadRequest(new { ok = false, error = "Failed to delete quiz" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("DeleteQuiz");
    }
}

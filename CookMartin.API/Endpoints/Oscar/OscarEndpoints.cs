using CookMartin.API.Models.Oscar;
using CookMartin.Oscar.Services.Interfaces;

namespace CookMartin.API.Endpoints.Oscar;

public static class OscarEndpoints
{
    public static void MapOscarEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/oscar")
            .AllowAnonymous()
            .WithTags("Oscar");

        group.MapGet("categories", async (IOscarService oscarService) =>
        {
            try
            {
                var categories = await oscarService.GetCategoriesWithNomineesAsync();
                return Results.Ok(new { ok = true, data = categories });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetOscarCategories");

        group.MapPost("picks", async (SubmitPicksRequest request, IOscarService oscarService) =>
        {
            try
            {
                await oscarService.SubmitPicksAsync(request.UserName, request.Picks);
                return Results.Ok(new { ok = true });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("SubmitOscarPicks");

        group.MapPost("admin/winner/{nomineeId:int}", async (int nomineeId, IOscarService oscarService) =>
        {
            try
            {
                await oscarService.SetWinnerAsync(nomineeId);
                return Results.Ok(new { ok = true });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("SetOscarWinner");

        group.MapGet("leaderboard", async (IOscarService oscarService) =>
        {
            try
            {
                var leaderboard = await oscarService.GetLeaderboardAsync();
                return Results.Ok(new { ok = true, data = leaderboard });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetOscarLeaderboard");

        group.MapGet("users/{userName}/results", async (string userName, IOscarService oscarService) =>
        {
            try
            {
                var results = await oscarService.GetUserResultsAsync(userName);
                return Results.Ok(new { ok = true, data = results });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetOscarUserResults");

        group.MapGet("submissions", async (IOscarService oscarService) =>
        {
            try
            {
                var submissions = await oscarService.GetSubmissionsAsync();
                return Results.Ok(new { ok = true, data = submissions });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("GetOscarSubmissions");
    }
}

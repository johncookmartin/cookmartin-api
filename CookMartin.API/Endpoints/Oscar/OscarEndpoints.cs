using CookMartin.API.Hubs;
using CookMartin.API.Models.Oscar;
using CookMartin.Oscar.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CookMartin.API.Endpoints.Oscar;

public static class OscarEndpoints
{
    public static void MapOscarEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/oscar")
            .AllowAnonymous()
            .WithTags("Oscar");

        var adminGroup = app.MapGroup("/oscar/admin")
            .RequireAuthorization()
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

        group.MapPost("picks", async (SubmitPicksRequest request, IOscarService oscarService, IHubContext<OscarHub> hubContext) =>
        {
            try
            {
                await oscarService.SubmitPicksAsync(request.UserName, request.Picks);
                await hubContext.Clients.All.SendAsync("LeaderboardChanged");
                await hubContext.Clients.All.SendAsync("SubmissionsChanged");
                await hubContext.Clients.All.SendAsync("ResultsChanged");
                return Results.Ok(new { ok = true });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("SubmitOscarPicks");

        adminGroup.MapPost("winner/{nomineeId:int}", async (int nomineeId, IOscarService oscarService, IHubContext<OscarHub> hubContext) =>
        {
            try
            {
                await oscarService.SetWinnerAsync(nomineeId);
                await hubContext.Clients.All.SendAsync("CategoriesChanged");
                await hubContext.Clients.All.SendAsync("LeaderboardChanged");
                await hubContext.Clients.All.SendAsync("ResultsChanged");
                return Results.Ok(new { ok = true });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("SetOscarWinner");

        adminGroup.MapDelete("winner/{nomineeId:int}", async (int nomineeId, IOscarService oscarService, IHubContext<OscarHub> hubContext) =>
        {
            try
            {
                await oscarService.ClearWinnerAsync(nomineeId);
                await hubContext.Clients.All.SendAsync("CategoriesChanged");
                await hubContext.Clients.All.SendAsync("LeaderboardChanged");
                await hubContext.Clients.All.SendAsync("ResultsChanged");
                return Results.Ok(new { ok = true });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { ok = false, error = ex.Message });
            }
        })
        .WithName("ClearOscarWinner");

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

using CookMartin.Blob.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookMartin.API.Endpoints;

public static class BlobEndpoints
{
    public static void MapBlobEndpoints(this WebApplication app)
    {
        app.MapPost("/api/blob/upload", async (
            IFormFile file,
            [FromQuery] string path,
            [FromKeyedServices("public")] IBlobService blobService) =>
        {
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest(new { error = "No file provided" });
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                return Results.BadRequest(new { error = "Path parameter is required" });
            }

            try
            {
                using var stream = file.OpenReadStream();
                var (url, blobPath) = await blobService.UploadAsync(path, stream);

                return Results.Ok(new { ok = true, url, path = blobPath });
            }
            catch (Azure.RequestFailedException ex) when (ex.Status == 401 || ex.Status == 403)
            {
                return Results.Json(
                    new { error = "Authentication or authorization failed", details = ex.Message },
                    statusCode: ex.Status);
            }
            catch (Azure.Identity.AuthenticationFailedException ex)
            {
                return Results.Json(
                    new { error = "Authentication failed", details = ex.Message },
                    statusCode: 401);
            }
        })
        .RequireAuthorization()
        .DisableAntiforgery()
        .WithName("UploadBlob")
        .WithTags("Blob Storage");
    }
}

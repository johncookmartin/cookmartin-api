using CookMartin.Blob.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookMartin.API.Endpoints;

public static class BlobEndpoints
{
    public static void MapBlobEndpoints(this WebApplication app)
    {
        app.MapPost("/api/blob/upload/pdf", async (
            IFormFile file,
            [FromQuery] string path,
            [FromKeyedServices("public")] IBlobService blobService) =>
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return Results.BadRequest(new { error = "Path parameter is required" });
            }

            return await UploadPdfAsync(file, path, blobService);
        })
        .RequireAuthorization()
        .DisableAntiforgery()
        .WithName("UploadPdfBlob")
        .WithTags("Blob Storage");

        app.MapPost("/api/blob/upload/john-resume", async (
            IFormFile file,
            [FromKeyedServices("public")] IBlobService blobService) =>
        {
            return await UploadPdfAsync(file, "john/John_Cook-Martin_Resume.pdf", blobService, generateQrCode: true);
        })
        .RequireAuthorization()
        .DisableAntiforgery()
        .WithName("UploadJohnResume")
        .WithTags("Blob Storage");

        app.MapPost("/api/blob/upload/jacquie-resume", async (
            IFormFile file,
            [FromKeyedServices("public")] IBlobService blobService) =>
        {
            return await UploadPdfAsync(file, "jacquie/Jacquie_Cook-Martin_Resume.pdf", blobService, generateQrCode: true);
        })
        .RequireAuthorization()
        .DisableAntiforgery()
        .WithName("UploadJacquieResume")
        .WithTags("Blob Storage");
    }

    private static async Task<IResult> UploadPdfAsync(IFormFile file, string path, IBlobService blobService, bool generateQrCode = false)
    {
        if (file == null || file.Length == 0)
        {
            return Results.BadRequest(new { error = "No file provided" });
        }

        if (!IsValidPdf(file))
        {
            return Results.BadRequest(new { error = "Invalid file. Only PDF files are allowed" });
        }

        try
        {
            using var stream = file.OpenReadStream();

            if (generateQrCode)
            {
                var (url, blobPath, qrCodeBase64) = await blobService.UploadReadablePdfAsync(path, stream, generateQrCode: true);
                return Results.Ok(new { ok = true, url, path = blobPath, qrCode = qrCodeBase64 });
            }
            else
            {
                var (url, blobPath) = await blobService.UploadReadablePdfAsync(path, stream);
                return Results.Ok(new { ok = true, url, path = blobPath });
            }
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
    }

    private static bool IsValidPdf(IFormFile file)
    {
        var allowedExtensions = new[] { ".pdf" };
        var allowedContentTypes = new[] { "application/pdf" };

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        var contentType = file.ContentType.ToLowerInvariant();

        return allowedExtensions.Contains(extension) && allowedContentTypes.Contains(contentType);
    }
}

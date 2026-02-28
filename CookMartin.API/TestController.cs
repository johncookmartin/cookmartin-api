using CookMartin.Blob.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookMartin.API;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok(new { ok = true, auth = "none" });
    }

    [Authorize]
    [HttpGet("private")]
    public IActionResult Private()
    {
        return Ok(new { ok = true, auth = "entra" });
    }

    [Authorize]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(
        IFormFile file,
        [FromQuery] string path,
        [FromKeyedServices("public")] IBlobService blobService)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { error = "No file provided" });
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            return BadRequest(new { error = "Path parameter is required" });
        }

        try
        {
            using var stream = file.OpenReadStream();
            var (url, blobPath) = await blobService.UploadAsync(path, stream);

            return Ok(new { ok = true, url, path = blobPath });
        }
        catch (Azure.RequestFailedException ex) when (ex.Status == 401 || ex.Status == 403)
        {
            return StatusCode(ex.Status, new { error = "Authentication or authorization failed", details = ex.Message });
        }
        catch (Azure.Identity.AuthenticationFailedException ex)
        {
            return StatusCode(401, new { error = "Authentication failed", details = ex.Message });
        }
    }

}

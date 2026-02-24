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
}

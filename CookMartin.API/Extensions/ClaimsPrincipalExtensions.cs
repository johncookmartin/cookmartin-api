using System.Security.Claims;

namespace CookMartin.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("oid")?.Value
            ?? user.FindFirst("sub")?.Value
            ?? "guest";
    }
}

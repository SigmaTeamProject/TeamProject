using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Extension;

public static class ClaimsPrincipalExtensions
{
    //to use it inject IHttpContextAccessor and use _contextAccessor.HttpContext.User.GetCurrentUserLogin()
    public static string GetCurrentUserLogin(this ClaimsPrincipal principal) 
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        var result = 
            principal.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value;
        if (result == null)
        {
            throw new UnauthorizedAccessException();
        }
        return result;
    }
}
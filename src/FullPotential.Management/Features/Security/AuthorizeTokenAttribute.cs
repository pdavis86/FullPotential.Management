using FullPotential.Management.Controllers;
using FullPotential.Management.Features.Users;

namespace FullPotential.Management.Features.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeTokenAttribute : Attribute
{
    public async Task<bool> IsTokenValid(HttpContext httpContext, IUserService userService)
    {
        var (username, token) = AppControllerBase.GetAuthorizationValues(httpContext.Request);

        if (userService == null || token == null)
        {
            return false;
        }

        return await userService.IsTokenValidAsync(username, token);
    }
}

using FullPotential.Management.Controllers;
using FullPotential.Management.Features.Users;

namespace FullPotential.Management.Features.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeTokenAttribute : Attribute
{
    public async Task<bool> IsTokenValid(HttpContext httpContext, IUserService userService)
    {
        var (username, token) = AppControllerBase.GetAuthorizationValues(httpContext.Request);

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        return (await userService.SignInWithTokenAsync(username, token))?.Token != null;
    }
}

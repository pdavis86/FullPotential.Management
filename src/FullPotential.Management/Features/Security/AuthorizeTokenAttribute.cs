namespace FullPotential.Management.Features.Security;

using FullPotential.Management.Controllers;
using FullPotential.Management.Features.Users;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeTokenAttribute : Attribute
{
    public async Task<bool> IsTokenValid(HttpContext httpContext, IUserService userService)
    {
        var (username, token) = AppControllerBase.GetAuthorizationValues(httpContext.Request);
        return await userService.IsTokenValidAsync(username, token);
    }
}

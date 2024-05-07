namespace FullPotential.Management.Features.Security;

using FullPotential.Management.Features.Users;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IUserService userService)
    {
        var authorizeAttribute = httpContext.GetEndpoint()?.Metadata.GetMetadata<AuthorizeTokenAttribute>();

        if (authorizeAttribute is not null && !await authorizeAttribute.IsTokenValid(httpContext, userService))
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _next(httpContext);
    }
}

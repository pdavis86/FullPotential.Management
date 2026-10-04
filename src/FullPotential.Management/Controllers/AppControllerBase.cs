using System.Text.Json;
using FullPotential.Management.Utilities;
using FullPotential.Persistence;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace FullPotential.Management.Controllers;

[EnableRateLimiting(SlidingWindowRateLimiter.PolicyName)]
public abstract class AppControllerBase : ControllerBase
{
    public const string AuthHeaderName = "X-Auth";
    public const string AuthHeaderDescription = "username;token";

    private static readonly JsonSerializerOptions UnityJsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = null
    };
    
    private GeneralDbContext _dbContext;

    protected AppControllerBase(GeneralDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [NonAction]
    protected string GetUsername()
    {
        return GetAuthorizationValues(Request).Username;
    }

    [NonAction]
    protected async Task<IUserContext> GetUserContextAsync()
    {
        var (username, token) = GetAuthorizationValues(Request);
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Token == token);
        return new UserContext
        {
            Id = user?.Id,
            Username = user?.Username
        };
    }

    public static (string Username, string Token) GetAuthorizationValues(HttpRequest request)
    {
        var headerValue = request.Headers[AuthHeaderName].ToString();
        var split = headerValue.Split(';');

        if (split.Length != 2)
        {
            return (string.Empty, string.Empty);
        }

        return (split[0], split[1]);
    }

    public static JsonResult UnityJsonResult(object? value)
    {
        return new JsonResult(value, UnityJsonSerializerOptions);
    }
}

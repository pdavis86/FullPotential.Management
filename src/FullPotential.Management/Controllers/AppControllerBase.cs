using System.Text.Json;
using FullPotential.Management.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

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

    [NonAction]
    protected string? GetUsername()
    {
        return GetAuthorizationValues(Request).Username;
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

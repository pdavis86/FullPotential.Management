namespace FullPotential.Management.Controllers;

using Microsoft.AspNetCore.Mvc;

public abstract class AppControllerBase : ControllerBase
{
    public const string AuthHeaderName = "X-Auth";
    public const string AuthHeaderDescription = "userName;token";

    [NonAction]
    public (string UserName, string Token) GetAuthorizationValues()
    {
        return GetAuthorizationValues(Request);
    }

    public static (string UserName, string Token) GetAuthorizationValues(HttpRequest request)
    {
        var headerValue = request.Headers[AuthHeaderName].ToString();
        var split = headerValue.Split(';');
        return (split[0], split[1]);
    }
}

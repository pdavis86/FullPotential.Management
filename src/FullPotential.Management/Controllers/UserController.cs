namespace FullPotential.Management.Controllers;

using System.Diagnostics.CodeAnalysis;
using FullPotential.Management.Features.Security;
using FullPotential.Management.Features.Users;
using FullPotential.Management.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("[controller]")]
public class UserController : AppControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("[action]")]
    [EnableRateLimiting(SlidingWindowRateLimiter.PolicyName)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> IsUserNameAvailable(string userName)
    {
        var result = await _userService.IsUserNameAvailableAsync(userName);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("[action]")]
    [EnableRateLimiting(SlidingWindowRateLimiter.PolicyName)]
    [ProducesResponseType<BadRequestWithReasonResult>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(string userName, string password)
    {
        var result = await _userService.RegisterAsync(userName, password);

        if (result != RegistrationResult.Success)
        {
            return new BadRequestWithReasonResult(result.ToString());
        }

        return Ok();
    }

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInWithPassword(string userName, string password)
    {
        var token = await _userService.SignInAsync(userName, password);

        if (token == null)
        {
            return BadRequest();
        }

        return Ok(token);
    }

    [HttpPost("[action]")]
    [AuthorizeToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public new async Task<IActionResult> SignOut()
    {
        var (userName, token) = GetAuthorizationValues();
        await _userService.SignOutAsync(userName, token);
        return Ok();
    }
}

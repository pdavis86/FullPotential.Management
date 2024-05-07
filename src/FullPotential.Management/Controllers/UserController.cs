namespace FullPotential.Management.Controllers;

using System.Diagnostics.CodeAnalysis;
using FullPotential.Management.Features.Security;
using FullPotential.Management.Features.Users;
using FullPotential.Management.Features.Users.Models;
using FullPotential.Management.Utilities;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> IsUsernameAvailable(string userName)
    {
        var result = await _userService.IsUsernameAvailableAsync(userName);

        if (!result)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost("[action]")]
    [ProducesResponseType<BadRequestWithReasonResult>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(string username, string password)
    {
        var result = await _userService.RegisterAsync(username, password);

        if (result != RegistrationResult.Success)
        {
            return new BadRequestWithReasonResult(result.ToString());
        }

        return Ok();
    }

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInWithPassword(SignIn model)
    {
        var token = await _userService.SignInAsync(model.Username, model.Password);

        if (token == null)
        {
            return BadRequest();
        }

        return Ok(token);
    }

    [AuthorizeToken]
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public new async Task<IActionResult> SignOut()
    {
        await _userService.ResetTokenAsync(GetUsername()!);
        return Ok();
    }
}

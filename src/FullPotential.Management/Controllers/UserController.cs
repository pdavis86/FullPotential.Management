using System.Diagnostics.CodeAnalysis;
using FullPotential.Management.Features.Security;
using FullPotential.Management.Features.Users;
using FullPotential.Management.Features.Users.Models;
using FullPotential.Management.Utilities.Models;
using Microsoft.AspNetCore.Mvc;

namespace FullPotential.Management.Controllers;

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
    public async Task<IActionResult> IsUsernameAvailable(string username)
    {
        var isAvailable = await _userService.IsUsernameAvailableAsync(username);

        return UnityJsonResult(new GenericResponse
        {
            IsSuccess = isAvailable
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(Credentials model)
    {
        var result = await _userService.RegisterAsync(model.Username, model.PasswordOrToken);

        return UnityJsonResult(new GenericResponse
        {
            IsSuccess = result == RegistrationResult.Success,
            ErrorCode = result != RegistrationResult.Success ? result.ToString() : null
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignInWithPassword(Credentials model)
    {
        var token = await _userService.SignInAsync(model.Username, model.PasswordOrToken);

        return UnityJsonResult(new GenericResponse
        {
            IsSuccess = !string.IsNullOrWhiteSpace(token),
            Result = token
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> IsTokenValid(Credentials model)
    {
        var isValid = await _userService.IsTokenValidAsync(model.Username, model.PasswordOrToken);
        return UnityJsonResult(new GenericResponse
        {
            IsSuccess = isValid
        });
    }

    [AuthorizeToken]
    [HttpGet("[action]")]
    public new async Task<IActionResult> SignOut()
    {
        await _userService.ResetTokenAsync(GetUsername()!);
        return UnityJsonResult(new GenericResponse
        {
            IsSuccess = true
        });
    }
}

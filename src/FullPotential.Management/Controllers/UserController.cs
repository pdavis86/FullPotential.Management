using System.Diagnostics.CodeAnalysis;

using FullPotential.Management.Features.Security;
using FullPotential.Management.Features.Users;
using FullPotential.Models.User;
using FullPotential.Models.Utilities;
using FullPotential.Persistence;

using Microsoft.AspNetCore.Mvc;

namespace FullPotential.Management.Controllers;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("[controller]")]
public class UserController : AppControllerBase
{
    private readonly IUserService _userService;

    public UserController(GeneralDbContext dbContext, IUserService userService)
        : base(dbContext)
    {
        _userService = userService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> IsUsernameAvailable(string username)
    {
        var isAvailable = await _userService.IsUsernameAvailableAsync(username);

        return UnityJsonResult(new GenericResponse(isAvailable));
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
        var userData = await _userService.SignInWithPasswordAsync(model.Username, model.PasswordOrToken);

        return UnityJsonResult(new GenericResponse
        {
            IsSuccess = !string.IsNullOrWhiteSpace(userData?.Token),
            Result = userData
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> IsTokenValid(Credentials model)
    {
        var userData = await _userService.SignInWithTokenAsync(model.Username, model.PasswordOrToken);
        return UnityJsonResult(new GenericResponse
        {
            IsSuccess = !string.IsNullOrWhiteSpace(userData?.Token),
            Result = userData
        });
    }

    [AuthorizeToken]
    [HttpGet("[action]")]
    public new async Task<IActionResult> SignOut()
    {
        await _userService.ResetTokenAsync(GetUsername());
        return UnityJsonResult(new GenericResponse(true));
    }
}

using System.Diagnostics.CodeAnalysis;

using FullPotential.Management.Features.Characters;
using FullPotential.Management.Features.Security;
using FullPotential.Models.Player;
using FullPotential.Models.Utilities;
using FullPotential.Persistence;

using Microsoft.AspNetCore.Mvc;

namespace FullPotential.Management.Controllers;

[ExcludeFromCodeCoverage]
[AuthorizeToken]
[ApiController]
[Route("[controller]")]
public class CharacterController : AppControllerBase
{
    private ICharacterService _characterService;

    public CharacterController(GeneralDbContext dbContext, ICharacterService characterService)
        : base(dbContext)
    {
        _characterService = characterService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCharacterData(string characterId)
    {
        var userContext = await GetUserContextAsync();
        var result = await _characterService.GetCharacterDataAsync(userContext, characterId);
        return UnityJsonResult(new GenericResponse(result));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SaveCharacterData(CharacterData model)
    {
        var userContext = await GetUserContextAsync();
        await _characterService.SaveCharacterDataAsync(userContext, model);
        return UnityJsonResult(new GenericResponse(true));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetInventoryData(string characterId, bool minimal = true)
    {
        var userContext = await GetUserContextAsync();
        var result = await _characterService.GetInventoryDataAsync(userContext, characterId, minimal);
        return UnityJsonResult(new GenericResponse(result));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SaveInventoryData(InventoryData model)
    {
        var userContext = await GetUserContextAsync();
        await _characterService.SaveInventoryDataAsync(userContext, model);
        return UnityJsonResult(new GenericResponse(true));
    }
}

using System.Diagnostics.CodeAnalysis;

using FullPotential.Management.Features.Instances;
using FullPotential.Management.Features.Security;
using FullPotential.Models.GameManagement;
using FullPotential.Models.Utilities;
using FullPotential.Persistence;

using Microsoft.AspNetCore.Mvc;

namespace FullPotential.Management.Controllers;

[ExcludeFromCodeCoverage]
[AuthorizeToken]
[ApiController]
[Route("[controller]")]
public class InstanceController : AppControllerBase
{
    private readonly IInstanceService _instanceService;

    public InstanceController(GeneralDbContext dbContext, IInstanceService instanceService)
        : base(dbContext)
    {
        _instanceService = instanceService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetConnectionDetails()
    {
        var userContext = await GetUserContextAsync();
        var result = await _instanceService.GetConnectionDetailsAsync(userContext);
        return UnityJsonResult(new GenericResponse(result));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SaveConnectionDetails(ConnectionDetails model)
    {
        var userContext = await GetUserContextAsync();
        await _instanceService.SaveConnectionDetailsAsync(userContext, model);
        return UnityJsonResult(new GenericResponse(true));
    }
}


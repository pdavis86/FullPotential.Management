namespace FullPotential.Management.Controllers;

using System.Diagnostics.CodeAnalysis;
using FullPotential.Management.Features.Instances;
using FullPotential.Management.Features.Security;
using Microsoft.AspNetCore.Mvc;

[ExcludeFromCodeCoverage]
[ApiController]
[Route("[controller]")]
public class InstanceController : AppControllerBase
{
    private readonly IInstanceService _instanceService;

    public InstanceController(IInstanceService instanceService)
    {
        _instanceService = instanceService;
    }

    [AuthorizeToken]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetConnectionDetails()
    {
        var result = await _instanceService.GetConnectionDetailsAsync(GetUsername()!);
        return UnityJsonResult(result);
    }
}


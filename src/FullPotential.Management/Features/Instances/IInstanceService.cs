using FullPotential.Management.Utilities;
using FullPotential.Models.GameManagement;

// Resharper disable UnusedParameter.Global

namespace FullPotential.Management.Features.Instances;

public interface IInstanceService
{
    Task<ConnectionDetails> GetConnectionDetailsAsync(IUserContext userContext);

    Task SaveConnectionDetailsAsync(IUserContext userContext, ConnectionDetails model);
}


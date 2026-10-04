using FullPotential.Management.Utilities;
using FullPotential.Models.GameManagement;
using FullPotential.Persistence;

using Microsoft.EntityFrameworkCore;

namespace FullPotential.Management.Features.Instances;

public class InstanceService : IInstanceService
{
    private readonly GeneralDbContext _dbContext;

    public InstanceService(
        GeneralDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ConnectionDetails> GetConnectionDetailsAsync(IUserContext userContext)
    {
        //todo: get an instance for the area of the universe where that user is
        var instance = await _dbContext.Instances.FirstOrDefaultAsync();

        if (instance != null)
        {
            var details = new ConnectionDetails
            {
                Status = (InstanceState)instance.State,
                Address = instance.Address,
                Port = instance.Port
            };

            return details;
        }

        //todo: If you didn't find one, start a new instance

        return new ConnectionDetails
        {
            Status = 0,
            Address = "127.0.0.1",
            Port = 7777
        };
    }

    public async Task SaveConnectionDetailsAsync(IUserContext userContext, ConnectionDetails model)
    {
        //todo: save an instance for the area of the universe where that user is

        await _dbContext.Instances.AddAsync(new Persistence.Entities.Instance
        {
            State = (int)model.Status,
            Address = model.Address,
            Port = model.Port
        });
    }
}

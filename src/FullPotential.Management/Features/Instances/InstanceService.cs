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

    public async Task<ConnectionDetails> GetConnectionDetailsAsync(string username)
    {
        //todo: get an instance for the area of the universe where that user is
        var instance = await _dbContext.Instances.FirstOrDefaultAsync();

        if (instance != null)
        {
            var details = new ConnectionDetails
            {
                Status = instance.State,
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
}

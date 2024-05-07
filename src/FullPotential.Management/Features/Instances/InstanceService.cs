namespace FullPotential.Management.Features.Instances;

public class InstanceService : IInstanceService
{
    public async Task<ConnectionDetails> GetConnectionDetailsAsync(string username)
    {
        //todo: implement
        await Task.Yield();
        return new ConnectionDetails
        {
            Status = InstanceState.Available,
            Address = "127.0.0.1",
            Port = 7777
        };
    }
}

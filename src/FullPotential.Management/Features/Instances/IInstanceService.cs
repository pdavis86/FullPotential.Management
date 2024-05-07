namespace FullPotential.Management.Features.Instances;

    public interface IInstanceService
    {
        Task<ConnectionDetails> GetConnectionDetailsAsync(string username);
    }


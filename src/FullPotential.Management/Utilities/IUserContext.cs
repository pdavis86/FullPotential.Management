
namespace FullPotential.Management.Utilities
{
    public interface IUserContext
    {
        Guid? Id { get; }

        string? Username { get; }
    }
}

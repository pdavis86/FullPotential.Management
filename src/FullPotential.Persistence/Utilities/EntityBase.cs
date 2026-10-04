// Resharper disable UnusedAutoPropertyAccessor.Global

namespace FullPotential.Persistence.Utilities;

public abstract class EntityBase : IEntity
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }
}

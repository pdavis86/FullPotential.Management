namespace FullPotential.Persistence.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }
}

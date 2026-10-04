using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class ItemEffect : EntityBase
{
    public required Item Item { get; set; }

    public required Guid EffectId { get; set; }
}

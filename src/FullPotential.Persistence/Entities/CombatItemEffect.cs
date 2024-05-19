using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class CombatItemEffect : EntityBase
{
    public required CombatItem CombatItem { get; set; }

    public required Guid EffectId { get; set; }
}

namespace FullPotential.Persistence.Entities;

using FullPotential.Persistence.Utilities;

public class CombatItemEffect : EntityBase
{
    public required CombatItem CombatItem { get; set; }

    public required Guid EffectId { get; set; }
}

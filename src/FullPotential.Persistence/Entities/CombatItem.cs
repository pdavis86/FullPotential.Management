using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class CombatItem : EntityBase
{
    public required Guid ItemId { get; set; }

    public bool IsTwoHanded { get; set; }

    public Guid? TargetingTypeId { get; set; }

    public Guid? TargetingVisualsTypeId { get; set; }

    public Guid? ShapeTypeId { get; set; }

    public Guid? ShapeVisualsTypeId { get; set; }

    public Guid? ValuePoolId { get; set; }

    public int? Ammo { get; set; }

    public ICollection<CombatItemEffect> Effects { get; } = new List<CombatItemEffect>();

    public Item Item { get; set; } = null!;
}

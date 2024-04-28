namespace FullPotential.Persistence.Entities;

using FullPotential.Persistence.Utilities;

public class CharacterEquippedItem : EntityBase
{
    public required Character Character { get; set; }

    public required Guid SlotId { get; set; }

    public required Guid ItemId { get; set; }
}

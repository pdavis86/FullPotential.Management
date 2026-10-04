using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class CharacterEquippedItem : EntityBase
{
    public required Character Character { get; set; }

    public required Guid ItemId { get; set; }

    public required string SlotId { get; set; }
}

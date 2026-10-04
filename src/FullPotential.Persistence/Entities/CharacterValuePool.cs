using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class CharacterValuePool : EntityBase
{
    public Guid CharacterId { get; set; }
    public required Character Character { get; set; }

    public required Guid Key { get; set; }

    public required int Value { get; set; }
}

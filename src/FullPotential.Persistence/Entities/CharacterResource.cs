namespace FullPotential.Persistence.Entities;

using FullPotential.Persistence.Utilities;

public class CharacterResource : EntityBase
{
    public required Character Character { get; set; }

    public required Guid ResourceId { get; set; }

    public required int Value { get; set; }
}

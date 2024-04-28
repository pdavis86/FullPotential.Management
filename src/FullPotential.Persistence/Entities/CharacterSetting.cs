
using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class CharacterSetting : EntityBase
{
    public required Character Character { get; set; }

    public required string Key { get; set; }

    public required string Value { get; set; }
}

using System.ComponentModel.DataAnnotations;
using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class CharacterSetting : EntityBase
{
    public required Character Character { get; set; }

    [MaxLength(256)]
    public required string Key { get; set; }

    [MaxLength(256)]
    public required string Value { get; set; }
}

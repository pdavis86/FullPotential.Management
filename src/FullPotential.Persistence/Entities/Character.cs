using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class Character : EntityBase
{
    public required User Owner { get; set; }

    public ICollection<CharacterSetting> Settings { get; } = new List<CharacterSetting>();

    public ICollection<CharacterEquippedItem> EquippedItems { get; } = new List<CharacterEquippedItem>();
}

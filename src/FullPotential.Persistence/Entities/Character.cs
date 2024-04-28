namespace FullPotential.Persistence.Entities;

using FullPotential.Persistence.Utilities;

public class Character : EntityBase
{
    public required User Owner { get; set; }

    public ICollection<CharacterResource> Resources { get; } = new List<CharacterResource>();

    public ICollection<CharacterSetting> Settings { get; } = new List<CharacterSetting>();

    public ICollection<CharacterEquippedItem> EquippedItems { get; } = new List<CharacterEquippedItem>();
}

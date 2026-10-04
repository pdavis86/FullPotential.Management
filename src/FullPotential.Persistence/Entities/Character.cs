using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class Character : EntityBase
{
    public required User User { get; set; }

    public ICollection<CharacterSetting> Settings { get; } = new List<CharacterSetting>();

    public ICollection<CharacterValuePool> ValuePools { get; } = new List<CharacterValuePool>();

    public ICollection<CharacterEquippedItem> EquippedItems { get; } = new List<CharacterEquippedItem>();
}

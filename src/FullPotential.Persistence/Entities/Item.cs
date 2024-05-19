using System.ComponentModel.DataAnnotations;
using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class Item : EntityBase
{
    public required Character Owner { get; set; }

    public required Guid RegistryTypeId { get; set; }

    [MaxLength(256)]
    public required string Name { get; set; }

    public Guid? VisualsTypeId { get; set; }

    public int? Count { get; set; }

    public ICollection<ItemAttribute> Attributes { get; } = new List<ItemAttribute>();

    public CombatItem? CombatItem { get; set; }
}

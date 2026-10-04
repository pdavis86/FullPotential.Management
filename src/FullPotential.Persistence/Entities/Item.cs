using System.ComponentModel.DataAnnotations;
using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class Item : EntityBase
{
    public required Character Character { get; set; }

    [MaxLength(256)]
    public required string Name { get; set; }

    public Guid? RegistryTypeId { get; set; }

    public ICollection<ItemAttribute> Attributes { get; } = new List<ItemAttribute>();

    public ICollection<ItemProperty> Properties { get; } = new List<ItemProperty>();

    public ICollection<ItemEffect> Effects { get; } = new List<ItemEffect>();
}

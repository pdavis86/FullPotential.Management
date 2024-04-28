namespace FullPotential.Persistence.Entities;

using FullPotential.Persistence.Utilities;

public class ItemAttribute : EntityBase
{
    public required Item Item { get; set; }

    public required string Key { get; set; }

    public required string Value { get; set; }
}

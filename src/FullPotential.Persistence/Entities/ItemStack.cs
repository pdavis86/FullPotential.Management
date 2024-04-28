namespace FullPotential.Persistence.Entities;

using FullPotential.Persistence.Utilities;

public class ItemStack : EntityBase
{
    public required Character Owner { get; set; }

    public required Guid RegistryTypeId { get; set; }

    public required string BaseName { get; set; }
    
    public required int Count { get; set; }
}

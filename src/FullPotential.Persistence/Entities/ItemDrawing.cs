namespace FullPotential.Persistence.Entities;

using FullPotential.Persistence.Utilities;

public class ItemDrawing : EntityBase
{
    public required Item Item { get; set; }
    
    public required string DrawingCode { get; set; }
}

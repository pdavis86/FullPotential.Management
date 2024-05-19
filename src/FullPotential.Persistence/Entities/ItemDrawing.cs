using System.ComponentModel.DataAnnotations;
using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class ItemDrawing : EntityBase
{
    public required Item Item { get; set; }
    
    [MaxLength(256)]
    public required string DrawingCode { get; set; }
}

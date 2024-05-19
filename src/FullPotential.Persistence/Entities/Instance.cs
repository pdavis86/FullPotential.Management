using System.ComponentModel.DataAnnotations;
using FullPotential.Persistence.Utilities;

namespace FullPotential.Persistence.Entities;

public class Instance : EntityBase
{
    public required int State { get; set; }

    [MaxLength(2048)]
    public required string Address { get; set; }

    public required int Port { get; set; }
}

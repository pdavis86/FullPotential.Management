namespace FullPotential.Management.Utilities.Models;

public class GenericResponse
{
    public bool IsSuccess { get; set; }

    public string? ErrorCode { get; set; }

    public string? Result { get; set; }
}


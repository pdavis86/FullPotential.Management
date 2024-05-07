namespace FullPotential.Management.Features.Instances;

public class ConnectionDetails
{
    public required InstanceState Status { get; set; }

    public required string Address { get; set; }

    public required int Port { get; set; }
}

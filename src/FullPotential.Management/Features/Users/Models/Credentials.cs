namespace FullPotential.Management.Features.Users.Models;

public class Credentials
{
    public required string Username { get; set; }
    
    public required string PasswordOrToken { get; set; }
}


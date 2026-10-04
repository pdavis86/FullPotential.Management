namespace FullPotential.Management.Utilities
{
    public class UserContext : IUserContext
    {
        public Guid? Id { get; set; }

        public string? Username { get; set; }
    }
}

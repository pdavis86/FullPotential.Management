namespace FullPotential.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GeneralDbContext>
{
    public GeneralDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<GeneralDbContext>();
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FullPotential;Trusted_Connection=True;TrustServerCertificate=true;");
        return new GeneralDbContext(optionsBuilder.Options);
    }
}

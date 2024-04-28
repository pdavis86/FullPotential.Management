namespace FullPotential.Persistence;

using FullPotential.Persistence.Entities;
using FullPotential.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

public sealed class GeneralDbContext : DbContext
{
    public GeneralDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public new void SaveChanges()
    {
        SetLastUpdated();
        base.SaveChanges();
    }

    public new async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetLastUpdated();
        await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var helper = new ModelCreationHelper();

        helper.OnModelCreating(modelBuilder.Entity<User>());
    }

    private void SetLastUpdated()
    {
        var addedOrModified = ChangeTracker.Entries()
            .Where(c => c.State is EntityState.Added or EntityState.Modified);

        foreach (var item in addedOrModified)
        {
            if (item.Entity is EntityBase entity)
            {
                entity.LastUpdated = DateTime.UtcNow;
            }
        }
    }
}

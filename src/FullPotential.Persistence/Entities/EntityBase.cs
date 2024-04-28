namespace FullPotential.Persistence.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime LastUpdated { get; set; }

    public static void OnModelCreating<T>(EntityTypeBuilder<T> entityTypeBuilder)
        where T : EntityBase
    {
        entityTypeBuilder
            .Property(b => b.Created)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}

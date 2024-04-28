namespace FullPotential.Persistence.Utilities;

using System.ComponentModel;
using System.Reflection;
using FullPotential.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ModelCreationHelper
{
    public void OnModelCreating<T>(EntityTypeBuilder<T> entityTypeBuilder)
        where T : EntityBase
    {
        AddEntityBaseFeatures(entityTypeBuilder);

        var entityType = typeof(T);

        AddConditionalIndex(entityType, entityTypeBuilder);

        var props = entityType.GetProperties();

        foreach (var prop in props)
        {
            if (prop == null)
            {
                continue;
            }

            AddDefaultValue(prop, entityTypeBuilder);
        }
    }

    private void AddEntityBaseFeatures<T>(EntityTypeBuilder<T> entityTypeBuilder)
        where T : EntityBase
    {
        entityTypeBuilder
            .Property(b => b.Created)
            .HasDefaultValueSql("GETUTCDATE()");
    }

    private void AddConditionalIndex<T>(Type entityType, EntityTypeBuilder<T> entityTypeBuilder)
        where T : EntityBase
    {
        var attribute = entityType.GetCustomAttribute<ConditionalIndexAttribute>();

        if (attribute == null)
        {
            return;
        }

        entityTypeBuilder
            .HasIndex(attribute.ColumnName)
            .IsUnique()
            .HasFilter(attribute.Filter);
    }

    private void AddDefaultValue<T>(PropertyInfo prop, EntityTypeBuilder<T> entityTypeBuilder)
        where T : EntityBase
    {
        var attribute = prop.GetCustomAttribute<DefaultValueAttribute>();

        if (attribute == null)
        {
            return;
        }

        entityTypeBuilder
            .Property(prop.PropertyType, prop.Name)
            .HasDefaultValue(attribute.Value);
    }
}

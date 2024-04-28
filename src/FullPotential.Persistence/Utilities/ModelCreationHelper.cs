namespace FullPotential.Persistence.Utilities;

using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ModelCreationHelper
{
    public void OnModelCreating(EntityTypeBuilder entityTypeBuilder)
    {
        AddEntityBaseFeatures(entityTypeBuilder);

        var entityType = entityTypeBuilder.Metadata.ClrType;

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

    private void AddEntityBaseFeatures(EntityTypeBuilder entityTypeBuilder)
    {
        entityTypeBuilder
            .Property(nameof(EntityBase.Created))
            .HasDefaultValueSql("GETUTCDATE()");
    }

    private void AddConditionalIndex(Type entityType, EntityTypeBuilder entityTypeBuilder)
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

    private void AddDefaultValue(PropertyInfo prop, EntityTypeBuilder entityTypeBuilder)
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

using FullPotential.Persistence.Entities;
using FullPotential.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;

// Resharper disable CSharpWarnings::CS8618
// Resharper disable UnusedMember.Global
// Resharper disable UnusedParameter.Local

namespace FullPotential.Persistence;

public sealed class GeneralDbContext : DbContext
{
    public GeneralDbContext(DbContextOptions options)
        : base(options)
    {
    }

    #region Sets

    public DbSet<User> Users { get; set; }

    public DbSet<Instance> Instances { get; set; }

    public DbSet<Character> Characters { get; set; }

    public DbSet<CharacterEquippedItem> CharacterEquippedItems { get; set; }

    public DbSet<CharacterSetting> CharacterSettings { get; set; }

    public DbSet<CharacterValuePool> CharacterValuePools { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<ItemAttribute> ItemAttributes { get; set; }

    public DbSet<ItemEffect> ItemEffects { get; set; }

    public DbSet<ItemProperty> ItemProperties { get; set; }

    #endregion

    #region Methods

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
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            helper.AdditionalEntitySetUp(modelBuilder.Entity(entityType.ClrType));
        }

        AddSpecialForeignKeys(modelBuilder);

        AddIndexes(modelBuilder);

        //AddSeedData(modelBuilder);
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

    #endregion

    private void AddSpecialForeignKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterSetting>()
            .HasOne(x => x.Character)
            .WithMany(x => x.Settings)
            .HasForeignKey(x => x.CharacterId);

        modelBuilder.Entity<CharacterValuePool>()
            .HasOne(x => x.Character)
            .WithMany(x => x.ValuePools)
            .HasForeignKey(x => x.CharacterId);
    }

    private static void AddIndexes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterSetting>()
            .HasIndex(x => new { x.CharacterId, x.Key })
            .IsUnique();
    }

    //private static void AddSeedData(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<InstanceState>().HasData([
    //        new InstanceState { Id = InstanceState.StartingUp, Name = nameof(InstanceState.StartingUp) },
    //        new InstanceState { Id = InstanceState.Available, Name = nameof(InstanceState.Available) },
    //        new InstanceState { Id = InstanceState.Full, Name = nameof(InstanceState.Full) }
    //    ]);
    //}
}

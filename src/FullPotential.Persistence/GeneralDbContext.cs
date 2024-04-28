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

    #region Sets

    public DbSet<Character> Characters { get; set; }

    public DbSet<CharacterEquippedItem> CharacterEquippedItems { get; set; }

    public DbSet<CharacterResource> CharacterResources { get; set; }

    public DbSet<CharacterSetting> CharacterSettings { get; set; }

    public DbSet<CombatItem> CombatItems { get; set; }

    public DbSet<CombatItemEffect> CombatItemEffects { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<ItemAttribute> ItemAttributes { get; set; }

    public DbSet<ItemDrawing> ItemDrawings { get; set; }

    public DbSet<ItemStack> ItemStacks { get; set; }

    public DbSet<User> Users { get; set; }

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
            helper.OnModelCreating(modelBuilder.Entity(entityType.ClrType));
        }
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
}

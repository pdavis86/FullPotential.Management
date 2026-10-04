
using FullPotential.Management.Utilities;
using FullPotential.Models.Player;
using FullPotential.Persistence;
using FullPotential.Persistence.Entities;

using Microsoft.EntityFrameworkCore;

namespace FullPotential.Management.Features.Characters;

public class CharacterService : ICharacterService
{
    private readonly GeneralDbContext _dbContext;

    public CharacterService(GeneralDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CharacterData> GetCharacterDataAsync(IUserContext userContext, string characterId)
    {
        var character = await _dbContext.Characters
            .Include(x => x.Settings)
            .Include(x => x.ValuePools)
            .FirstOrDefaultAsync(x => x.User.Id == userContext.Id && x.Id.ToString() == characterId);

        if (character == null)
        {
            throw new UnauthorizedAccessException();
        }

        return new CharacterData
        {
            CharacterId = character.Id.ToString(),
            Settings = character.Settings.ToDictionary(x => x.Key, x => x.Value),
            ValuePools = character.ValuePools.ToDictionary(x => x.Key.ToString(), x => x.Value)
        };
    }

    public async Task SaveCharacterDataAsync(IUserContext userContext, CharacterData model)
    {
        var character = await _dbContext.Characters
            .Include(x => x.Settings)
            .Include(x => x.ValuePools)
            .FirstOrDefaultAsync(x => x.User.Id == userContext.Id && x.Id.ToString() == model.CharacterId);

        if (character == null)
        {
            if (model.CharacterId != null)
            {
                throw new UnauthorizedAccessException();
            }

            var user = await GetUserAsync(userContext);
            character = new Character { User = user };
            _dbContext.Characters.Add(character);
        }

        SynchronizeCharacterSettings(character, model);
        SynchronizeCharacterValuePools(character, model);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<InventoryData> GetInventoryDataAsync(IUserContext userContext, string characterId, bool minimal = true)
    {
        var character = await _dbContext.Characters
            .Include(x => x.EquippedItems)
            .FirstOrDefaultAsync(x => x.User.Id == userContext.Id && x.Id.ToString() == characterId);

        if (character == null)
        {
            throw new UnauthorizedAccessException();
        }

        var equippedItems = character.EquippedItems.ToDictionary(x => x.SlotId.ToString(), x => x.ItemId.ToString());

        List<Item> characterItems;
        if (minimal)
        {
            var equippedItemIds = equippedItems.Select(x => x.Value).ToList();
            characterItems = _dbContext.Items
                .Include(x => x.Attributes)
                .Include(x => x.Properties)
                .Include(x => x.Effects)
                .Where(x => x.Character == character && equippedItemIds.Contains(x.Id.ToString()))
                .ToList();
        }
        else
        {
            characterItems = _dbContext.Items
                .Include(x => x.Attributes)
                .Include(x => x.Properties)
                .Include(x => x.Effects)
                .Where(x => x.Character == character)
                .ToList();
        }

        return new InventoryData
        {
            CharacterId = character.Id.ToString(),
            Items = characterItems.Select(x => GetItemData(character, x)).ToList(),
            EquippedItems = equippedItems
        };
    }

    public async Task SaveInventoryDataAsync(IUserContext userContext, InventoryData model)
    {
        var character = await _dbContext.Characters
            .Include(x => x.EquippedItems)
            .FirstOrDefaultAsync(x => x.User.Id == userContext.Id && x.Id.ToString() == model.CharacterId);

        if (character == null)
        {
            // Fall-back for local load with HTTPS save
            character = _dbContext.Characters
                .Include(x => x.EquippedItems)
                .FirstOrDefault(x => x.User.Id == userContext.Id && x.User.Username == model.CharacterId);

            if (character == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Fix the model
            model.CharacterId = character.Id.ToString();
            foreach (var item in model.Items)
            {
                item.CharacterId = character.Id.ToString();
            }
        }

        SynchronizeCharacterEquippedItems(character, model);

        await UpdateItemsAsync(character, model);

        await _dbContext.SaveChangesAsync();
    }

    private async Task<User> GetUserAsync(IUserContext userContext)
    {
        return await _dbContext.Users.FirstAsync(x => x.Id == userContext.Id);
    }

    private void SynchronizeCharacterSettings(Character character, CharacterData model)
    {
        if (model.Settings == null)
        {
            return;
        }

        var expectedKeys = model.Settings.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => x.Key);
        var keysToDelete = character.Settings.Select(s => s.Key).Except(expectedKeys).ToList();
        foreach (var key in keysToDelete)
        {
            character.Settings.Remove(character.Settings.Single(x => x.Key == key));
        }

        foreach (var kvp in model.Settings)
        {
            character.Settings.MatchOrOtherwise(
                x => x.Key == kvp.Key,
                match => match.Value = kvp.Value,
                () => _dbContext.CharacterSettings.Add(new CharacterSetting
                {
                    Character = character,
                    Key = kvp.Key,
                    Value = kvp.Value
                }));
        }
    }

    private void SynchronizeCharacterValuePools(Character character, CharacterData model)
    {
        var keysToDelete = character.ValuePools.Select(v => v.Key.ToString()).Except(model.ValuePools.Keys).ToList();
        foreach (var key in keysToDelete)
        {
            character.ValuePools.Remove(character.ValuePools.Single(x => x.Key.ToString() == key));
        }

        foreach (var kvp in model.ValuePools)
        {
            character.ValuePools.MatchOrOtherwise(
                x => x.Key.ToString() == kvp.Key,
                match => match.Value = kvp.Value,
                () => _dbContext.CharacterValuePools.Add(new CharacterValuePool
                {
                    Character = character,
                    Key = new Guid(kvp.Key),
                    Value = kvp.Value
                }));
        }
    }

    private async Task UpdateItemsAsync(Character character, InventoryData model)
    {
        var idList = model.Items.Select(x => x.Id.ToString());
        var characterItems = _dbContext.Items
            .Include(x => x.Attributes)
            .Include(x => x.Properties)
            .Include(x => x.Effects)
            .Where(x => x.Character == character && idList.Contains(x.Id.ToString()))
            .ToList();

        //var newItems = new Dictionary<Item, ItemData>();

        foreach (var itemData in model.Items)
        {
            var match = characterItems.FirstOrDefault(x => x.Id.ToString() == itemData.Id);

            if (match != null)
            {
                if (itemData.IsDeleted)
                {
                    _dbContext.Items.Remove(match);
                }
                else
                {
                    match.UpdateFromDto(itemData);
                }
            }
            else
            {
                var newItem = ItemFactory.CreateFromDto(character, itemData);
                newItem.UpdateDependenciesFromDto(itemData);
                //newItems.Add(newItem, itemData);
                _dbContext.Items.Add(newItem);
            }
        }

        await Task.CompletedTask;
        //await _dbContext.SaveChangesAsync();

        //foreach (var kvp in newItems)
        //{
        //    kvp.Key.UpdateDependenciesFromDto(kvp.Value);
        //}
    }

    private void SynchronizeCharacterEquippedItems(Character character, InventoryData model)
    {
        if (model.EquippedItems == null)
        {
            return;
        }

        var expectedKeys = model.EquippedItems.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => x.Key);
        var keysDelete = character.EquippedItems.Select(x => x.SlotId.ToString()).Except(expectedKeys).ToList();
        foreach (var key in keysDelete)
        {
            character.EquippedItems.Remove(character.EquippedItems.Single(x => x.SlotId.ToString() == key));
        }

        // todo: check the item exists

        foreach (var kvp in model.EquippedItems.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
        {
            character.EquippedItems.MatchOrOtherwise(
                x => x.SlotId == kvp.Key,
                match => match.ItemId = new Guid(kvp.Value),
                () => _dbContext.CharacterEquippedItems.Add(new CharacterEquippedItem
                {
                    Character = character,
                    SlotId = kvp.Key,
                    ItemId = new Guid(kvp.Value)
                }));
        }
    }

    private ItemData GetItemData(Character character, Item item)
    {
        return new ItemData
        {
            Id = item.Id.ToString(),
            CharacterId = character.Id.ToString(),
            RegistryTypeId = item.RegistryTypeId?.ToString(),
            Name = item.Name,
            Attributes = item.Attributes.ToDictionary(x => x.Key, x => x.Value),
            Properties = item.Properties.ToDictionary(x => x.Key, x => x.Value),
            EffectIds = item.Effects.Select(x => x.EffectId.ToString()).ToList()
        };
    }
}

using FullPotential.Management.Utilities;
using FullPotential.Models.Player;
using FullPotential.Persistence.Entities;

namespace FullPotential.Management.Features.Characters
{
    public static class ItemFactory
    {
        public static void UpdateFromDto(this Item item, ItemData itemData)
        {
            item.Name = itemData.Name;
            item.UpdateDependenciesFromDto(itemData);
        }

        public static void UpdateDependenciesFromDto(this Item item, ItemData itemData)
        {
            SynchronizeAttributes(item, itemData);
            SynchronizeProperties(item, itemData);
            SynchronizeEffects(item, itemData);
        }

        public static Item CreateFromDto(Character character, ItemData itemData)
        {
            var item = new Item
            {
                Character = character,
                Name = itemData.Name,
                RegistryTypeId = !string.IsNullOrWhiteSpace(itemData.RegistryTypeId) ? new Guid(itemData.RegistryTypeId) : null,
            };

            return item;
        }

        private static void SynchronizeAttributes(Item entity, ItemData model)
        {
            if (model.Attributes == null)
            {
                return;
            }

            var expectedKeys = model.Attributes.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => x.Key);
            var keysToDelete = entity.Attributes.Select(x => x.Key).Except(expectedKeys).ToList();
            foreach (var key in keysToDelete)
            {
                entity.Attributes.Remove(entity.Attributes.Single(x => x.Key == key));
            }

            foreach (var kvp in model.Attributes.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
            {
                entity.Attributes.MatchOrOtherwise(
                    x => x.Key == kvp.Key,
                    match => match.Value = kvp.Value,
                    () => entity.Attributes.Add(new ItemAttribute { Item = entity, Key = kvp.Key, Value = kvp.Value }));
            }
        }

        private static void SynchronizeProperties(Item entity, ItemData model)
        {
            if (model.Properties == null)
            {
                return;
            }

            var expectedKeys = model.Properties.Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => x.Key);
            var keysToDelete = entity.Properties.Select(x => x.Key).Except(expectedKeys).ToList();
            foreach (var key in keysToDelete)
            {
                entity.Properties.Remove(entity.Properties.Single(x => x.Key == key));
            }

            foreach (var kvp in model.Properties.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
            {
                entity.Properties.MatchOrOtherwise(
                    x => x.Key == kvp.Key,
                    match => match.Value = kvp.Value,
                    () => entity.Properties.Add(new ItemProperty { Item = entity, Key = kvp.Key, Value = kvp.Value }));
            }
        }

        private static void SynchronizeEffects(Item entity, ItemData model)
        {
            if (model.EffectIds == null)
            {
                return;
            }

            var idsToDelete = entity.Effects.Select(x => x.Id.ToString()).Except(model.EffectIds).ToList();
            foreach (var id in idsToDelete)
            {
                entity.Effects.Remove(entity.Effects.Single(x => x.Id.ToString() == id));
            }

            foreach (var effectId in model.EffectIds)
            {
                entity.Effects.MatchOrOtherwise(
                    x => x.Id.ToString() == effectId,
                    _ => { },
                    () => entity.Effects.Add(new ItemEffect { Item = entity, EffectId = new Guid(effectId) }));
            }
        }
    }
}

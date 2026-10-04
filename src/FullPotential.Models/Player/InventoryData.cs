using System.Collections.Generic;

namespace FullPotential.Models.Player
{
    public class InventoryData
    {
        public string CharacterId { get; set; }

        public List<ItemData> Items { get; set; }

        public Dictionary<string, string> EquippedItems { get; set; }

        public bool IsDirty { get; set; }
    }
}

using System.Collections.Generic;

// Resharper disable UnusedAutoPropertyAccessor.Global
// Resharper disable UnusedMember.Global

namespace FullPotential.Models.Player
{
    public class ItemData
    {
        public string Id { get; set; }

        public string CharacterId { get; set; }

        public string RegistryTypeId { get; set; }
        
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public List<string> EffectIds { get; set; }
    }
}

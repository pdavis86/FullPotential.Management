using System.Collections.Generic;

namespace FullPotential.Models.Player
{
    public class CharacterData
    {
        public string CharacterId { get; set; }
        
        public Dictionary<string, string> Settings { get; set; }

        public Dictionary<string, int> ValuePools { get; set; }
    }
}

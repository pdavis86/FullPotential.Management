using FullPotential.Management.Utilities;
using FullPotential.Models.Player;

namespace FullPotential.Management.Features.Characters;

public interface ICharacterService
{
    Task<CharacterData> GetCharacterDataAsync(IUserContext userContext, string characterId);

    Task SaveCharacterDataAsync(IUserContext userContext, CharacterData model);

    Task<InventoryData> GetInventoryDataAsync(IUserContext userContext, string characterId, bool minimal = true);

    Task SaveInventoryDataAsync(IUserContext userContext, InventoryData model);
}

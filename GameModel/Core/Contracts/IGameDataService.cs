using GameModel.Infrastructure.Network.Dtos;

namespace GameModel.Core.Contracts
{
    public interface IGameDataService
    {
        Task<IEnumerable<string>> GetAvailableCharactersAsync();
        Task<GenshinCharacterDto?> GetCharacterDetailsAsync(string name);
    }
}
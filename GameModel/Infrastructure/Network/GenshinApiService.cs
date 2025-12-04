using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GameModel.Core.Contracts;
using GameModel.Infrastructure.Network.Dtos;

namespace GameModel.Infrastructure.Network
{
    public class GenshinApiService : IGameDataService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://genshin.jmp.blue/characters";

        public GenshinApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<string>> GetAvailableCharactersAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(BaseUrl);
                return JsonSerializer.Deserialize<List<string>>(response) ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<GenshinCharacterDto?> GetCharacterDetailsAsync(string name)
        {
            try
            {
                var url = $"{BaseUrl}/{name.ToLower()}";
                var response = await _httpClient.GetStringAsync(url);
                return JsonSerializer.Deserialize<GenshinCharacterDto>(response);
            }
            catch
            {
                return null; // Character not found or API error
            }
        }
    }
}
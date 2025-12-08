using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GameModel.Core.Contracts;
using GameModel.Infrastructure.Network.Dtos;
using GameModel.Infrastructure.IO; // Added namespace

namespace GameModel.Infrastructure.Network
{
    public class GenshinApiService : IGameDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ImageCacheService _imageCache; // Dependency
        private const string BaseUrl = "https://genshin.jmp.blue/characters";

        public GenshinApiService(ImageCacheService imageCache)
        {
            _httpClient = new HttpClient();
            _imageCache = imageCache;
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
                Console.WriteLine($"api error: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<GenshinCharacterDto?> GetCharacterDetailsAsync(string name)
        {
            try
            {
                var url = $"{BaseUrl}/{name.ToLower()}";
                var response = await _httpClient.GetStringAsync(url);
                var dto = JsonSerializer.Deserialize<GenshinCharacterDto>(response);

                if (dto != null)
                {
                    // trigger caching process in background or await it
                    string localPath = await _imageCache.GetCachedImagePathAsync(name);
                    // we could add this path to DTO if we extended it
                    Console.WriteLine($"[system] image cached at: {localPath}");
                }

                return dto;
            }
            catch
            {
                return null; 
            }
        }
    }
}
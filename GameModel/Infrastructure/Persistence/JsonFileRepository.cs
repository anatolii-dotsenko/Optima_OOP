using System;
using System.IO;
using System.Text.Json;
using GameModel.Core.Data;

namespace GameModel.Infrastructure.Persistence
{
    public class JsonFileRepository
    {
        private readonly string _filePath;

        public JsonFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(SaveBase data)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                // У реальному проекті тут варто використати ILogger для помилок
                Console.WriteLine($"Failed to save game: {ex.Message}");
            }
        }

        public SaveBase Load()
        {
            if (!File.Exists(_filePath)) return new SaveBase();
            
            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<SaveBase>(json) ?? new SaveBase();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game: {ex.Message}");
                return new SaveBase();
            }
        }
    }
}
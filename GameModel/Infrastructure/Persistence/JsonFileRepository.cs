using GameModel.Core.Data;
using System.Text.Json;

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
                // In a real project, it is recommended to use ILogger for errors here
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
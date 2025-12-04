using System.Text.Json.Serialization;

namespace GameModel.Infrastructure.Network.Dtos
{
    // DTO for character list names
    public class CharacterListDto : List<string> { }

    // DTO for detailed Genshin character information
    public class GenshinCharacterDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("vision")]
        public string Vision { get; set; } = string.Empty; // Element (e.g. Pyro, Hydro)

        [JsonPropertyName("weapon")]
        public string Weapon { get; set; } = string.Empty; // Sword, Claymore, Catalyst...

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
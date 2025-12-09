namespace GameModel.Core.Data
{
    public class SaveBase
    {
        public DateTime SaveDate { get; set; }
        public List<CharacterData> Characters { get; set; } = new();
        // other aspects can be added, world state or history
    }
}
// manages a dictionary of stats and provides methods for modification
namespace GameModel.Core.ValueObjects
{
    public class CharacterStats
    {
        private readonly Dictionary<StatType, int> _stats = new();

        public CharacterStats() { }

        // Copy constructor
        public CharacterStats(CharacterStats other)
        {
            foreach (var kvp in other._stats)
            {
                _stats[kvp.Key] = kvp.Value;
            }
        }

        public int GetStat(StatType type) => _stats.GetValueOrDefault(type, 0);

        public void SetStat(StatType type, int value) => _stats[type] = value;

        public void ModifyStat(StatType type, int delta)
        {
            int current = GetStat(type);
            _stats[type] = current + delta;
        }
        public Dictionary<StatType, int> ToDictionary()
        {
            return new Dictionary<StatType, int>(_stats);
        }
    }
}
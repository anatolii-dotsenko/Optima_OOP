using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Abilities
{
    public class Lightning : Ability
    {
        public Lightning() : base("Lightning") { }

        protected override int CalculateDamage(CharacterStats userStats, CharacterStats targetStats)
        {
            // Example logic: Lightning deals moderate damage but pierces some armor
            // (Assuming magic usually ignores armor, we can add flavor here)
            int magicPower = userStats.GetStat(StatType.Attack); // Or a specific Magic stat if added
            
            // Base 50 damage + scaling
            return 50 + magicPower;
        }
    }
}
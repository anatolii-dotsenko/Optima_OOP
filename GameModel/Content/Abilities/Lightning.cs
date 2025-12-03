using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Abilities
{
    public class Lightning : Ability
    {
        public Lightning() : base("Lightning") { }

        public override int CalculateDamage(CharacterStats userStats, CharacterStats targetStats)
        {
            // Magic damage ignores armor in this implementation
            return 65; 
        }
    }
}
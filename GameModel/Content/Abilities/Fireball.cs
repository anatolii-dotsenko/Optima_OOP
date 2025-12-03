using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Abilities
{
    public class Fireball : Ability
    {
        public Fireball() : base("Fireball") { }

        public override int CalculateDamage(CharacterStats userStats, CharacterStats targetStats)
        {
            // Magic damage ignores armor in this implementation
            return 75; 
        }
    }
}
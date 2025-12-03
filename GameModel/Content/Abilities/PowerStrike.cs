using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Abilities
{
    public class PowerStrike : Ability
    {
        public PowerStrike() : base("Power Strike") { }

        public override int CalculateDamage(CharacterStats userStats, CharacterStats targetStats)
        {
            int attack = userStats.GetStat(StatType.Attack);
            int armor = targetStats.GetStat(StatType.Armor);
            
            // Double damage but subject to armor
            int rawDamage = attack * 2;
            return Math.Max(0, rawDamage - armor);
        }
    }
}
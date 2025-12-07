using System;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Abilities
{
    public class PowerStrike : Ability
    {
        public PowerStrike() : base("Power Strike") { }

        protected override int CalculateDamage(CharacterStats userStats, CharacterStats targetStats)
        {
            int attack = userStats.GetStat(StatType.Attack);
            int armor = targetStats.GetStat(StatType.Armor);
            
            // Logic: Double damage but subject to flat armor
            int rawDamage = attack * 2;
            
            // Returns max of 0 or damage minus armor (No percentage resistance)
            return Math.Max(0, rawDamage - armor);
        }
    }
}
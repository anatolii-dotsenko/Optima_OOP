using System;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Abilities
{
    public class Lightning : Ability
    {
        public Lightning() : base("Lightning") { }

        protected override int CalculateDamage(CharacterStats userStats, CharacterStats targetStats)
        {
            // Magic Formula: (Base * Multiplier) + Bonus
            // Multiplier = 1 - Resistance + Penetration (Min 0)

            int baseDmg = 50; // Moderate base damage for Lightning

            // Fetch stats (converted to percentage where necessary)
            double magicRes = targetStats.GetStat(StatType.MagicResist) / 100.0;
            double penetration = userStats.GetStat(StatType.Penetration) / 100.0;
            int bonus = userStats.GetStat(StatType.MagicPower);

            // Calculate Multiplier
            double multiplier = Math.Max(0, 1.0 - magicRes + penetration);

            // Final Calculation
            return (int)((baseDmg * multiplier) + bonus);
        }
    }
}
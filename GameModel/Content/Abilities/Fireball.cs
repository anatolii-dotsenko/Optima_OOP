using System;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Abilities
{
    public class Fireball : Ability
    {
        public Fireball() : base("Fireball") { }

        protected override int CalculateDamage(CharacterStats userStats, CharacterStats targetStats)
        {
            // Magic Formula: (Base * Multiplier) + Bonus
            // Multiplier = 1 - Resistance + Penetration (Min 0)

            int baseDmg = 75; // Base damage of the spell

            // Fetch stats (Default to 0 if not present)
            // Note: Division by 100.0 is crucial to convert integer stats (e.g., 20) to percentages (0.2)
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
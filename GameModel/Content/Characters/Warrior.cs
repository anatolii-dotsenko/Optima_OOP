// represents a character preset with high health and physical defense
using GameModel.Content.Abilities;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Characters
{
    public class Warrior : Character
    {
        public Warrior(string name) : base(name)
        {
            SetBaseStat(StatType.MaxHealth, 130);
            SetBaseStat(StatType.Health, 130);
            SetBaseStat(StatType.Attack, 15);
            SetBaseStat(StatType.Armor, 10);
            LearnAbility(new PowerStrike());
        }
    }
}
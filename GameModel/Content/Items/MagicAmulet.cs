using GameModel.Content.Abilities;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Items
{
    public class MagicAmulet : Item
    {
        public MagicAmulet() : base("Amulet of Fire")
        {
            AddModifier(StatType.MaxHealth, 20);
            // In a real app, this might heal the user on equip, or MaxHp logic would handle current HP ratio
            GrantedAbility = new Fireball();
        }
    }
}
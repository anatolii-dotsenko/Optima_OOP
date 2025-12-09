using GameModel.Content.Abilities;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Content.Items
{
    public class LightningWand : Item
    {
        public LightningWand() : base("Lightning Wand")
        {
            // wands might give less raw attack than swords, but grant powerful spells
            AddModifier(StatType.Attack, 5);

            // this item grants the new ability
            GrantedAbility = new Lightning();
        }
    }
}
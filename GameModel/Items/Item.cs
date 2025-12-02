using GameModel.Abilities;

namespace GameModel.Items
{
    /// <summary>
    /// Base class for all items.
    /// For advanced systems you can derive Weapon, Armor, Amulet, etc.
    /// </summary>
    public abstract class Item
    {
        public string Name { get; }

        public int AttackBonus { get; protected set; }
        public int ArmorBonus { get; protected set; }
        public int HealthBonus { get; protected set; }

        public Ability? GrantedAbility { get; protected set; }

        protected Item(
            string name, 
            int attack = 0, 
            int armor = 0, 
            int health = 0, 
            Ability? ability = null)
        {
            Name = name;
            AttackBonus = attack;
            ArmorBonus = armor;
            HealthBonus = health;
            GrantedAbility = ability;
        }
    }
}

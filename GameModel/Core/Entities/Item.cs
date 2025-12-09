using GameModel.Core.ValueObjects;

namespace GameModel.Core.Entities
{
    public abstract class Item
    {
        public string Name { get; }
        public Ability? GrantedAbility { get; protected set; }

        // Dictionary of stat modifiers (StatType -> Amount)
        public Dictionary<StatType, int> Modifiers { get; } = new();

        protected Item(string name)
        {
            Name = name;
        }

        protected void AddModifier(StatType type, int amount)
        {
            if (Modifiers.ContainsKey(type))
                Modifiers[type] += amount;
            else
                Modifiers[type] = amount;
        }
    }
}
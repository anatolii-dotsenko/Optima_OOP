namespace GameModel.Characters
{
    /// <summary>
    /// Abstract base class for stat modifiers.
    /// Allows extensibility without modifying Item or Character classes.
    /// </summary>
    public abstract class StatModifier
    {
        public abstract void Apply(CharacterStats stats);
    }

    /// <summary>
    /// Modifier for increasing Attack stat.
    /// </summary>
    public class AttackBonus : StatModifier
    {
        private readonly int _amount;

        public AttackBonus(int amount)
        {
            _amount = amount;
        }

        public override void Apply(CharacterStats stats)
        {
            stats.Attack += _amount;
        }
    }

    /// <summary>
    /// Modifier for increasing Armor stat.
    /// </summary>
    public class ArmorBonus : StatModifier
    {
        private readonly int _amount;

        public ArmorBonus(int amount)
        {
            _amount = amount;
        }

        public override void Apply(CharacterStats stats)
        {
            stats.Armor += _amount;
        }
    }

    /// <summary>
    /// Modifier for increasing Health stat.
    /// </summary>
    public class HealthBonus : StatModifier
    {
        private readonly int _amount;

        public HealthBonus(int amount)
        {
            _amount = amount;
        }

        public override void Apply(CharacterStats stats)
        {
            stats.Health += _amount;
        }
    }
}

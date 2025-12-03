using GameModel.Characters;

namespace GameModel.Combat
{
    /// <summary>
    /// Represents a planned combat action with its effects.
    /// Separates action calculation from execution.
    /// </summary>
    public class CombatAction
    {
        public enum ActionType { PhysicalDamage, MagicalDamage, Heal, Buff, Debuff }

        public ActionType Type { get; set; }
        public int Amount { get; set; }
        public Character Target { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

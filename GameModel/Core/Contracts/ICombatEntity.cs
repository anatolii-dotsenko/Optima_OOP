namespace GameModel.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for any entity that participates in combat.
    /// </summary>
    public interface ICombatEntity
    {
        string Name { get; }
        int CurrentHealth { get; }
        int MaxHealth { get; }
        bool IsAlive { get; }
        
        void TakeDamage(int amount);
        void Heal(int amount);
    }
}

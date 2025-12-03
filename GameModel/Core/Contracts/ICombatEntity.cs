using GameModel.Core.ValueObjects;
using GameModel.Core.Entities;

namespace GameModel.Core.Contracts
{
    public interface ICombatEntity
    {
        string Name { get; }
        bool IsAlive { get; }
        
        // State mutation methods
        void TakeDamage(int amount);
        void Heal(int amount);
        
        // Data retrieval
        CharacterStats GetStats();
        IEnumerable<Ability> GetAbilities();
    }
}
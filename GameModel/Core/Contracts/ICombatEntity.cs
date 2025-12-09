using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Contracts
{
    public interface ICombatEntity
    {
        string Name { get; }
        bool IsAlive { get; }

        // state mutation methods
        void TakeDamage(int amount);
        void Heal(int amount);

        // data retrieval
        CharacterStats GetStats();
        IEnumerable<Ability> GetAbilities();
    }
}
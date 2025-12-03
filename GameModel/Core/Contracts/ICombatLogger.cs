using GameModel.Core.ValueObjects;

namespace GameModel.Core.Contracts
{
    public interface ICombatLogger
    {
        void LogAttack(AttackResult result);
        void LogAbility(AbilityResult result);
        void LogHeal(HealResult result);
        void LogMessage(string message);
    }
}
using GameModel.Core.ValueObjects;

namespace GameModel.Core.Entities.States
{
    public class AliveState : ICharacterState
    {
        public bool CanAct() => true;

        public void TakeDamage(Character context, int amount)
        {
            int current = context.StatsInternal.GetStat(StatType.Health);
            int newHealth = Math.Max(0, current - amount);
            context.StatsInternal.SetStat(StatType.Health, newHealth);

            if (newHealth <= 0)
            {
                // transition to dead state
                context.SetState(new DeadState());
            }
        }

        public void Heal(Character context, int amount)
        {
            int current = context.StatsInternal.GetStat(StatType.Health);
            int max = context.StatsInternal.GetStat(StatType.MaxHealth);
            context.StatsInternal.SetStat(StatType.Health, Math.Min(max, current + amount));
        }
    }
}
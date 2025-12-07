namespace GameModel.Core.Entities.States
{
    public class DeadState : ICharacterState
    {
        public bool CanAct() => false; // Dead characters cannot act

        public void TakeDamage(Character context, int amount)
        {
            // Do nothing, already dead.
        }

        public void Heal(Character context, int amount)
        {
            // Logic: Can only be revived by special items (not implemented here), 
            // regular heal might not work or transitions back to AliveState if amount > 0.
            if (amount > 0)
            {
                context.StatsInternal.SetStat(GameModel.Core.ValueObjects.StatType.Health, amount);
                context.SetState(new AliveState());
            }
        }
    }
}
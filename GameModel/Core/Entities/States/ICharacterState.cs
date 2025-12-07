namespace GameModel.Core.Entities.States
{
    /// <summary>
    /// Pattern: State.
    /// Defines the interface for encapsulating behavior associated with a particular state of the Character.
    /// </summary>
    public interface ICharacterState
    {
        bool CanAct();
        void TakeDamage(Character context, int amount);
        void Heal(Character context, int amount);
    }
}
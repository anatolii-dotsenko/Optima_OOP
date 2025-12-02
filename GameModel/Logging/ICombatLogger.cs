namespace GameModel.Logging
{
    /// <summary>
    /// Basic logging interface for combat information.
    /// Enables switching between console, file, UI, etc.
    /// </summary>
    public interface ICombatLogger
    {
        void Write(string message);
    }
}

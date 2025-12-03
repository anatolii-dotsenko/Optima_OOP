namespace GameModel.Logging
{
    /// <summary>
    /// Generic logging interface following industry standard practice.
    /// Decouples logging from specific domain concepts (combat, game, etc.).
    /// </summary>
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogDebug(string message);
    }
}

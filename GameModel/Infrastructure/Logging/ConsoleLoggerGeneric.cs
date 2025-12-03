using System;

namespace GameModel.Logging
{
    /// <summary>
    /// Generic console logger for system-wide events.
    /// Not specific to combat; can log any game event.
    /// </summary>
    public class ConsoleLoggerGeneric : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"[WARN] {message}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }

        public void LogDebug(string message)
        {
            Console.WriteLine($"[DEBUG] {message}");
        }
    }
}

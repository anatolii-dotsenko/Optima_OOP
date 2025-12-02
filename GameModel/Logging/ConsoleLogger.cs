using System;

namespace GameModel.Logging
{
    /// <summary>
    /// Simple implementation that logs to the console.
    /// </summary>
    public class ConsoleLogger : ICombatLogger
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}

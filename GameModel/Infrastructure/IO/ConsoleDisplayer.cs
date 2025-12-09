using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.IO
{
    public class ConsoleDisplayer : IDisplayer
    {
        public void Write(string message) => Console.Write(message);
        public void WriteLine(string message) => Console.WriteLine(message);
        public string ReadLine() => Console.ReadLine() ?? string.Empty;
        public void Clear() => Console.Clear();
    }
}
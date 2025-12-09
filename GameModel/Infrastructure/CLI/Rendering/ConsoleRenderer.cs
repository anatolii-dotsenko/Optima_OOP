using GameModel.Core.Data;
using GameModel.Core.Entities;

namespace GameModel.Infrastructure.CLI.Rendering
{
    // Concrete implementation specifically for System.Console
    public class ConsoleRenderer : IConsoleRenderer, IRenderer<CharacterData>, IRenderer<Item>
    {
        public void WriteMessage(string message) => Console.WriteLine(message);

        public void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }

        public void DrawSeparator() => Console.WriteLine(new string('-', 40));

        // rendering implementation for CharacterData
        public void Render(CharacterData data)
        {
            DrawSeparator();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"CHARACTER: {data.Name} [{data.ClassType}]");
            Console.ResetColor();
            Console.WriteLine($"HP: {data.CurrentHealth} | Status: {(data.CurrentHealth > 0 ? "Alive" : "Dead")}");

            Console.WriteLine("Stats:");
            foreach (var stat in data.BaseStats)
            {
                Console.WriteLine($"  - {stat.Key}: {stat.Value}");
            }
            DrawSeparator();
        }

        // rendering implementation for Item
        public void Render(Item item)
        {
            Console.WriteLine($"[ITEM] {item.Name}");
            // Additional fancy item rendering logic here
        }
    }
}
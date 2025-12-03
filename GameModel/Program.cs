using GameModel.Infrastructure.Setup;

namespace GameModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Game Model...");
            
            var builder = new GameBuilder();
            var (battleManager, commands, characters) = builder.Build();

            // Example: Run a command manually
            Console.WriteLine("\n--- Command Test ---");
            var attackCmd = commands.GetCommand("attack");
            attackCmd?.Execute(new[] { "Thorin", "Elira" });

            // Run automated battle
            Console.WriteLine("\n--- Automated Battle ---");
            battleManager.StartBattle();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
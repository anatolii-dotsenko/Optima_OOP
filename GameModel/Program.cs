using System;
using System.Linq;
using GameModel.Infrastructure.CLI;
using GameModel.Infrastructure.Setup;

namespace GameModel
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Build the Object Graph
            var builder = new GameBuilder();
            var (registry, worldContext, docContext) = builder.Build();

            // 2. Select Session based on args or input
            ICliSession session;

            if (args.Length > 0 && args[0] == "--text")
            {
                session = new TextSession(registry);
            }
            else if (args.Length > 0 && args[0] == "--chars")
            {
                session = new CharacterSession(registry);
            }
            else
            {
                // Simple interactive selection
                Console.WriteLine("Select Mode: 1. Text, 2. Characters");
                var choice = Console.ReadLine();
                if (choice == "1") session = new TextSession(registry);
                else session = new CharacterSession(registry);
            }

            // 3. Run Engine
            var engine = new CliEngine(session);
            engine.Run();
        }
    }
}
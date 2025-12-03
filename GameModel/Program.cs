using System;
using GameModel.Infrastructure.CLI;
using GameModel.Infrastructure.Setup;

namespace GameModel
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new GameBuilder();
            // Receive separate registries
            var (textRegistry, charRegistry, worldContext, docContext) = builder.Build();

            ICliSession session;

            if (args.Length > 0 && args[0] == "--text")
            {
                session = new TextSession(textRegistry);
            }
            else if (args.Length > 0 && args[0] == "--chars")
            {
                session = new CharacterSession(charRegistry);
            }
            else
            {
                Console.WriteLine("Select Mode: 1. Text, 2. Characters");
                var choice = Console.ReadLine();
                if (choice == "1") session = new TextSession(textRegistry);
                else session = new CharacterSession(charRegistry);
            }

            var engine = new CliEngine(session);
            engine.Run();
        }
    }
}
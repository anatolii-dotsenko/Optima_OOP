using System;
using GameModel.Infrastructure.CLI;

namespace GameModel
{
    class Program
    {
        static void Main(string[] args)
        {
            ICliSession session;

            if (args.Length > 0)
            {
                if (args[0] == "--text") session = new TextSession();
                else if (args[0] == "--chars") session = new CharacterSession();
                else session = SelectModeInteractive();
            }
            else
            {
                session = SelectModeInteractive();
            }

            var engine = new CliEngine(session);
            engine.Run();
        }

        static ICliSession SelectModeInteractive()
        {
            Console.WriteLine("Select Mode:");
            Console.WriteLine("1. Text Mode");
            Console.WriteLine("2. Characters Mode");
            Console.Write("Choice (1-2): ");
            
            var input = Console.ReadLine();
            if (input == "1") return new TextSession();
            if (input == "2") return new CharacterSession();
            
            Console.WriteLine("Defaulting to Characters Mode.");
            return new CharacterSession();
        }
    }
}
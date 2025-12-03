using System;
using GameModel.Characters;

namespace GameModel.Commands
{
    /// <summary>
    /// Command to display character status. Auto-discovered and registered via reflection.
    /// Demonstrates OCP: new commands added without modifying existing code.
    /// </summary>
    public class StatusCommand : ICommand
    {
        public string Keyword => "status";
        public string Description => "Display character status. Usage: status <character_name>";

        private readonly GameState _gameState;

        public StatusCommand()
        {
            _gameState = GameState.Instance;
        }

        public void Execute(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: status <character_name>");
                return;
            }

            var character = _gameState.GetCharacter(args[0]);
            if (character == null)
            {
                Console.WriteLine("Character not found.");
                return;
            }

            var (atk, arm, maxHp) = character.GetFinalStats();
            Console.WriteLine($"\n--- {character.Name} ---");
            Console.WriteLine($"HP: {character.Health}/{maxHp}");
            Console.WriteLine($"ATK: {atk}");
            Console.WriteLine($"ARM: {arm}");
            Console.WriteLine();
        }
    }
}

using GameModel.Characters;
using GameModel.Combat;

namespace GameModel.Commands
{
    /// <summary>
    /// Command to heal a character: heal healer_name amount
    /// </summary>
    public class HealCommand : ICommand
    {
        public string Keyword => "heal";
        public string Description => "Heal a character. Usage: heal <character_name> <amount>";

        private readonly CombatSystem _combatSystem;
        private readonly GameState _gameState;

        public HealCommand()
        {
            _gameState = GameState.Instance;
            _combatSystem = _gameState.CombatSystem;
        }

        public void Execute(string[] args)
        {
            if (args.Length < 2 || !int.TryParse(args[1], out int amount))
            {
                Console.WriteLine("Usage: heal <character_name> <amount>");
                return;
            }

            var healer = _gameState.GetCharacter(args[0]);

            if (healer == null)
            {
                Console.WriteLine("Error: Character not found.");
                return;
            }

            _combatSystem.Heal(healer, amount);
        }
    }
}

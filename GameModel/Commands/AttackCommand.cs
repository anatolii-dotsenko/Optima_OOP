using GameModel.Characters;
using GameModel.Combat;

namespace GameModel.Commands
{
    /// <summary>
    /// Command to perform a basic attack: attack attacker_name target_name
    /// </summary>
    public class AttackCommand : ICommand
    {
        public string Keyword => "attack";
        public string Description => "Attack a target. Usage: attack <attacker_name> <target_name>";

        private readonly CombatSystem _combatSystem;
        private readonly GameState _gameState;

        public AttackCommand()
        {
            // Dependencies are resolved via GameState singleton or service locator pattern
            _gameState = GameState.Instance;
            _combatSystem = _gameState.CombatSystem;
        }

        public void Execute(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: attack <attacker_name> <target_name>");
                return;
            }

            var attacker = _gameState.GetCharacter(args[0]);
            var target = _gameState.GetCharacter(args[1]);

            if (attacker == null || target == null)
            {
                Console.WriteLine("Error: Character not found.");
                return;
            }

            _combatSystem.Attack(attacker, target);
        }
    }
}

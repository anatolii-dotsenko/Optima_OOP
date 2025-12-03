using GameModel.Characters;
using GameModel.Combat;

namespace GameModel.Commands
{
    /// <summary>
    /// Command to execute an ability: ability user_name target_name ability_name
    /// </summary>
    public class AbilityCommand : ICommand
    {
        public string Keyword => "ability";
        public string Description => "Use an ability. Usage: ability <user_name> <target_name> <ability_name>";

        private readonly CombatSystem _combatSystem;
        private readonly GameState _gameState;

        public AbilityCommand()
        {
            _gameState = GameState.Instance;
            _combatSystem = _gameState.CombatSystem;
        }

        public void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: ability <user_name> <target_name> <ability_name>");
                return;
            }

            var user = _gameState.GetCharacter(args[0]);
            var target = _gameState.GetCharacter(args[1]);
            var abilityName = args[2];

            if (user == null || target == null)
            {
                Console.WriteLine("Error: Character not found.");
                return;
            }

            _combatSystem.UseAbility(user, target, abilityName);
        }
    }
}

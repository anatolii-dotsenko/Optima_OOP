using System;
using System.Collections.Generic;
using GameModel.Characters;
using GameModel.Logging;
using GameModel.Combat.Actions;

namespace GameModel.Combat
{
    /// <summary>
    /// Manages the flow of turn-based combat between participants.
    /// Encapsulates battle loop logic, win conditions, and turn coordination.
    /// This is the authoritative "Coordinator" referenced in the Technical Specification.
    /// Coordinates the flow of battle between attacker and defender autonomously.
    /// </summary>
    public class BattleManager
    {
        private readonly CombatSystem _combatSystem;
        private readonly ICombatLogger _logger;
        private readonly List<Character> _participants;
        private int _currentTurnIndex;
        private bool _battleInProgress;

        public BattleManager(CombatSystem combatSystem, ICombatLogger logger)
        {
            _combatSystem = combatSystem;
            _logger = logger;
            _participants = new();
            _currentTurnIndex = 0;
            _battleInProgress = false;
        }

        /// <summary>
        /// Adds a participant to the battle.
        /// </summary>
        public void AddParticipant(Character character)
        {
            if (character == null)
                throw new ArgumentNullException(nameof(character));
            _participants.Add(character);
        }

        /// <summary>
        /// Starts the battle and runs the turn-based loop until a winner is determined.
        /// This method coordinates the entire flow of battle autonomously.
        /// </summary>
        public void Start()
        {
            if (_participants.Count < 2)
            {
                _logger.LogAbilityNotFound("BattleManager", "Insufficient participants for battle.");
                return;
            }

            _battleInProgress = true;
            _currentTurnIndex = 0;

            Console.WriteLine("=== BATTLE STARTS ===\n");

            while (_battleInProgress && HasActiveCombatants())
            {
                ExecuteTurn();
            }

            Console.WriteLine("\n=== BATTLE ENDS ===\n");
            DisplayBattleResult();
        }

        /// <summary>
        /// Executes a single turn for the current participant.
        /// </summary>
        private void ExecuteTurn()
        {
            var currentParticipant = _participants[_currentTurnIndex % _participants.Count];

            // Skip dead participants
            if (currentParticipant.Health <= 0)
            {
                _currentTurnIndex++;
                return;
            }

            // Execute automated action for the current participant
            ExecuteAutoAction(currentParticipant);

            _currentTurnIndex++;
        }

        /// <summary>
        /// Executes a predetermined action for the participant.
        /// </summary>
        private void ExecuteAutoAction(Character actor)
        {
            var opponent = GetOpponent(actor);
            if (opponent == null || opponent.Health <= 0)
                return;

            // Simple AI: use abilities when available, otherwise attack
            if (actor.Abilities.Count > 0 && _currentTurnIndex % 2 == 1)
            {
                var ability = actor.Abilities[_currentTurnIndex % actor.Abilities.Count];
                _combatSystem.UseAbility(actor, opponent, ability);
            }
            else
            {
                _combatSystem.Attack(actor, opponent);
            }
        }

        /// <summary>
        /// Determines if there are still active (alive) combatants.
        /// </summary>
        private bool HasActiveCombatants()
        {
            int aliveCombatants = 0;
            foreach (var participant in _participants)
            {
                if (participant.Health > 0)
                    aliveCombatants++;
            }
            return aliveCombatants > 1;
        }

        /// <summary>
        /// Gets the opposing participant (assumes 1v1 for simplicity).
        /// </summary>
        private Character GetOpponent(Character actor)
        {
            foreach (var participant in _participants)
            {
                if (participant != actor && participant.Health > 0)
                    return participant;
            }
            return null;
        }

        /// <summary>
        /// Displays the battle result and determines the winner.
        /// </summary>
        private void DisplayBattleResult()
        {
            Character winner = null;
            foreach (var participant in _participants)
            {
                if (participant.Health > 0)
                {
                    winner = participant;
                    break;
                }
            }

            if (winner != null)
            {
                Console.WriteLine($"*** {winner.Name} has won the battle! ***\n");
            }
            else
            {
                Console.WriteLine("*** Battle ended in a draw! ***\n");
            }
        }
    }
}

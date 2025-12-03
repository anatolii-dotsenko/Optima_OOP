using GameModel.Core.Contracts;
using GameModel.Core.Entities;

namespace GameModel.Systems.Combat
{
    public class BattleManager
    {
        private readonly ICombatSystem _combatSystem;
        private readonly ICombatLogger _logger;
        private readonly List<ICombatEntity> _participants = new();

        public BattleManager(ICombatSystem combatSystem, ICombatLogger logger)
        {
            _combatSystem = combatSystem;
            _logger = logger;
        }

        public void AddParticipant(ICombatEntity entity) => _participants.Add(entity);

        public void StartBattle()
        {
            if (_participants.Count < 2)
            {
                _logger.LogMessage("Not enough participants.");
                return;
            }

            _logger.LogMessage("=== BATTLE STARTED ===");
            int turn = 0;

            while (_participants.Count(p => p.IsAlive) > 1)
            {
                var attacker = _participants[turn % _participants.Count];
                if (!attacker.IsAlive)
                {
                    turn++;
                    continue;
                }

                var target = _participants.FirstOrDefault(p => p != attacker && p.IsAlive);
                if (target == null) break;

                TakeTurn(attacker, target);
                turn++;
            }

            var winner = _participants.FirstOrDefault(p => p.IsAlive);
            _logger.LogMessage($"=== BATTLE ENDED. Winner: {winner?.Name ?? "None"} ===");
        }

        private void TakeTurn(ICombatEntity attacker, ICombatEntity target)
        {
            // Simple AI Strategy: If ability available, use it (50% chance), else attack
            var abilities = attacker.GetAbilities().ToList();
            var random = new Random();

            if (abilities.Any() && random.NextDouble() > 0.5)
            {
                var ability = abilities[random.Next(abilities.Count)];
                _combatSystem.UseAbility(attacker, target, ability);
            }
            else
            {
                _combatSystem.Attack(attacker, target);
            }
        }
    }
}
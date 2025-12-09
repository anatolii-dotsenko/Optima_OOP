// orchestrates the turn-based battle loop and turn order
using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;

namespace GameModel.Systems.Combat
{
    public class BattleManager
    {
        private readonly ICombatSystem _combatSystem;
        private readonly ICombatLogger _logger;
        private readonly List<ICombatEntity> _participants = new();

        // turn history for logging purposes
        public List<string> BattleHistory { get; } = new();

        public BattleManager(ICombatSystem combatSystem, ICombatLogger logger)
        {
            _combatSystem = combatSystem;
            _logger = logger;
        }

        public void AddParticipant(ICombatEntity entity) => _participants.Add(entity);

        public void StartBattle()
        {
            // sort by Speed stat descending
            _participants.Sort((a, b) => b.GetStats().GetStat(StatType.Speed).CompareTo(a.GetStats().GetStat(StatType.Speed)));

            int turnCount = 1;
            while (_participants.Count(p => p.IsAlive) > 1)
            {
                LogTurnHeader(turnCount);

                foreach (var actor in _participants)
                {
                    if (!actor.IsAlive) continue;

                    // Simple target selection (first alive enemy)
                    var target = _participants.FirstOrDefault(p => p != actor && p.IsAlive);
                    if (target == null) break;

                    ExecuteTurn(actor, target);
                }
                turnCount++;
            }
        }

        private void ExecuteTurn(ICombatEntity actor, ICombatEntity target)
        {
            // ai vs player decision could be implemented here
            _combatSystem.Attack(actor, target);

            // record action in battle history
            BattleHistory.Add($"[{DateTime.Now:HH:mm:ss}] {actor.Name} acted against {target.Name}");
        }

        private void LogTurnHeader(int turn)
        {
            string msg = $"--- TURN {turn} ---";
            _logger.LogMessage(msg);
            BattleHistory.Add(msg);
        }
    }
}
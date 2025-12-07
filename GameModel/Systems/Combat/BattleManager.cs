using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Core.Contracts;
using GameModel.Core.Entities;
using GameModel.Core.ValueObjects;

namespace GameModel.Systems.Combat
{
    public class BattleManager
    {
        private readonly ICombatSystem _combatSystem;
        private readonly ICombatLogger _logger;
        private readonly List<ICombatEntity> _participants = new();
        
        // History of turns
        public List<string> BattleHistory { get; } = new();

        public BattleManager(ICombatSystem combatSystem, ICombatLogger logger)
        {
            _combatSystem = combatSystem;
            _logger = logger;
        }

        public void AddParticipant(ICombatEntity entity) => _participants.Add(entity);

        public void StartBattle()
        {
            // Sort by Speed for turn order
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
            // Simple AI vs Player check could go here
            _combatSystem.Attack(actor, target);
            
            // Record to history
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
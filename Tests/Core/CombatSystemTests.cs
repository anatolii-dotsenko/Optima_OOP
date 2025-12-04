using System;
using Xunit;
using Moq;
using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;
using GameModel.Systems.Combat;

namespace Optima_OOP.Tests.Core
{
    public class CombatSystemTests
    {
        [Fact]
        public void Attack_ShouldCalculateDamageCorrectly_AndLogResult()
        {
            Console.WriteLine("\n--- [Test Start] Verify damage calculation and logging logic ---");

            // Arrange
            var mockLogger = new Mock<ICombatLogger>();
            var combatSystem = new CombatSystem(mockLogger.Object);

            var mockAttacker = new Mock<ICombatEntity>();
            var attackerStats = new CharacterStats();
            attackerStats.SetStat(StatType.Attack, 20);
            mockAttacker.Setup(a => a.GetStats()).Returns(attackerStats);
            mockAttacker.Setup(a => a.Name).Returns("Hero");

            var mockDefender = new Mock<ICombatEntity>();
            var defenderStats = new CharacterStats();
            defenderStats.SetStat(StatType.Armor, 5);
            mockDefender.Setup(d => d.GetStats()).Returns(defenderStats);
            mockDefender.Setup(d => d.Name).Returns("Enemy");

            int expectedDamage = 15; // 20 - 5

            // Act
            combatSystem.Attack(mockAttacker.Object, mockDefender.Object);

            // Assert
            mockDefender.Verify(d => d.TakeDamage(expectedDamage), Times.Once);
            mockLogger.Verify(l => l.LogAttack(It.Is<AttackResult>(r => r.Damage == expectedDamage)), Times.Once);

            Console.WriteLine($"--- [Test Passed] Damage calculated as {expectedDamage} and logged ---");
        }
    }
}
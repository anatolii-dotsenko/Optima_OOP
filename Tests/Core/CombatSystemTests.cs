using Xunit;
using Moq;
using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;
using GameModel.Systems.Combat;

namespace Tests.Core
{
    public class CombatSystemTests
    {
        [Fact]
        public void Attack_ShouldCalculateDamageCorrectly_AndLogResult()
        {
            // Arrange
            // Mock the logger to verify interaction later
            var mockLogger = new Mock<ICombatLogger>();
            var combatSystem = new CombatSystem(mockLogger.Object);

            // Mock the Attacker
            var mockAttacker = new Mock<ICombatEntity>();
            var attackerStats = new CharacterStats();
            attackerStats.SetStat(StatType.Attack, 20); // Attack Power = 20
            mockAttacker.Setup(a => a.GetStats()).Returns(attackerStats);
            mockAttacker.Setup(a => a.Name).Returns("Hero");

            // Mock the Defender
            var mockDefender = new Mock<ICombatEntity>();
            var defenderStats = new CharacterStats();
            defenderStats.SetStat(StatType.Armor, 5);   // Armor = 5
            mockDefender.Setup(d => d.GetStats()).Returns(defenderStats);
            mockDefender.Setup(d => d.Name).Returns("Enemy");

            // Expected damage: 20 (Attack) - 5 (Armor) = 15
            int expectedDamage = 15;

            // Act
            combatSystem.Attack(mockAttacker.Object, mockDefender.Object);

            // Assert
            // Verify that TakeDamage was called on the defender with the correct amount
            mockDefender.Verify(d => d.TakeDamage(expectedDamage), Times.Once);

            // Verify that the logger was called to log the attack event
            mockLogger.Verify(l => l.LogAttack(It.Is<AttackResult>(r => 
                r.Damage == expectedDamage && 
                r.Attacker == "Hero" && 
                r.Target == "Enemy"
            )), Times.Once);
        }
    }
}
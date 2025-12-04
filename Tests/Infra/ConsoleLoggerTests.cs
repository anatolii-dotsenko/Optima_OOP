using Xunit;
using Moq;
using GameModel.Infrastructure.Logging;
using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;

namespace Tests.Infra
{
    public class ConsoleLoggerTests
    {
        [Fact]
        public void LogAttack_ShouldFormatMessage_AndCallDisplayer()
        {
            // Arrange
            // Mock the displayer (interface for Console I/O)
            var mockDisplayer = new Mock<IDisplayer>();
            
            // Inject the mock into the concrete ConsoleLogger
            var logger = new ConsoleLogger(mockDisplayer.Object);

            var attackResult = new AttackResult("Warrior", "Orc", 15);

            // Act
            logger.LogAttack(attackResult);

            // Assert
            // The logic inside ConsoleLogger uses CombatFormatter.
            // We expect the output: "{Attacker} attacks {Target} for {Damage} damage."
            string expectedMessage = "Warrior attacks Orc for 15 damage.";
            
            // Verify that WriteLine was called exactly once with the specific message
            mockDisplayer.Verify(d => d.WriteLine(expectedMessage), Times.Once);
        }
    }
}
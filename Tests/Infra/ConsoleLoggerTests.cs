using System;
using Xunit;
using Moq;
using GameModel.Infrastructure.Logging;
using GameModel.Core.Contracts;
using GameModel.Core.ValueObjects;

namespace Optima_OOP.Tests.Infra
{
    public class ConsoleLoggerTests
    {
        [Fact]
        public void LogAttack_ShouldFormatMessage_AndCallDisplayer()
        {
            Console.WriteLine("\n--- [Test Start] Verify ConsoleLogger formats output via IDisplayer ---");

            // Arrange
            var mockDisplayer = new Mock<IDisplayer>();
            var logger = new ConsoleLogger(mockDisplayer.Object);

            var attackResult = new AttackResult("Warrior", "Orc", 15);
            string expectedMessage = "Warrior attacks Orc for 15 damage.";

            // Act
            logger.LogAttack(attackResult);

            // Assert
            mockDisplayer.Verify(d => d.WriteLine(expectedMessage), Times.Once);

            Console.WriteLine($"--- [Test Passed] Logger correctly sent '{expectedMessage}' to displayer ---");
        }
    }
}
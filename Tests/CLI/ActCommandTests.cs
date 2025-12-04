using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Core.Contracts;
using GameModel.Core.State;
using GameModel.Content.Characters;

namespace Optima_OOP.Tests.CLI
{
    public class ActCommandTests
    {
        [Fact]
        public void Execute_AttackCommand_ShouldFindCharacters_AndCallCombatSystem()
        {
            Console.WriteLine("\n--- [Test Start] Verify 'act' command triggers combat system ---");

            // Arrange
            var mockCombatSystem = new Mock<ICombatSystem>();
            var worldContext = new WorldContext();

            var warrior = new Warrior("Conan");
            var mage = new Mage("Merlin");
            worldContext.Characters.Add(warrior);
            worldContext.Characters.Add(mage);

            var command = new ActCommand(mockCombatSystem.Object, worldContext);
            string[] args = new[] { "attack", "Conan", "Merlin" };
            var options = new Dictionary<string, string>();

            // Act
            command.Execute(args, options);

            // Assert
            mockCombatSystem.Verify(cs => cs.Attack(warrior, mage), Times.Once);
            
            Console.WriteLine("--- [Test Passed] Combat system was called correctly ---");
        }

        [Fact]
        public void Execute_WithUnknownCharacters_ShouldNotCallCombatSystem()
        {
            Console.WriteLine("\n--- [Test Start] Ensure no combat actions occur if characters don't exist ---");

            // Arrange
            var mockCombatSystem = new Mock<ICombatSystem>();
            var worldContext = new WorldContext(); // Empty world

            var command = new ActCommand(mockCombatSystem.Object, worldContext);
            string[] args = new[] { "attack", "Ghost", "Phantom" };

            // Act
            // This will print "Error: Actor or Target character not found." to console
            command.Execute(args, new Dictionary<string, string>());

            // Assert
            mockCombatSystem.Verify(cs => cs.Attack(It.IsAny<ICombatEntity>(), It.IsAny<ICombatEntity>()), Times.Never);
            
            Console.WriteLine("--- [Test Passed] System correctly handled missing characters ---");
        }
    }
}
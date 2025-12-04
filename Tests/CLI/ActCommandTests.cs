using Xunit;
using Moq;
using System.Collections.Generic;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Core.Contracts;
using GameModel.Core.State;
using GameModel.Content.Characters;

namespace Tests.CLI
{
    public class ActCommandTests
    {
        [Fact]
        public void Execute_AttackCommand_ShouldFindCharacters_AndCallCombatSystem()
        {
            // Arrange
            var mockCombatSystem = new Mock<ICombatSystem>();
            var worldContext = new WorldContext();

            // Populate context with real objects for simplicity (or use Mocks if preferred)
            var warrior = new Warrior("Conan");
            var mage = new Mage("Merlin");
            worldContext.Characters.Add(warrior);
            worldContext.Characters.Add(mage);

            // Create the command with dependencies
            var command = new ActCommand(mockCombatSystem.Object, worldContext);

            // Simulate user input arguments: "act attack Conan Merlin"
            // Note: The first arg passed to Execute is usually the sub-command or parameters
            string[] args = new[] { "attack", "Conan", "Merlin" };
            var options = new Dictionary<string, string>();

            // Act
            command.Execute(args, options);

            // Assert
            // Verify that the CombatSystem.Attack method was called with the correct entities
            mockCombatSystem.Verify(cs => cs.Attack(warrior, mage), Times.Once);
        }

        [Fact]
        public void Execute_WithUnknownCharacters_ShouldNotCallCombatSystem()
        {
            // Arrange
            var mockCombatSystem = new Mock<ICombatSystem>();
            var worldContext = new WorldContext(); // Empty world

            var command = new ActCommand(mockCombatSystem.Object, worldContext);
            string[] args = new[] { "attack", "Ghost", "Phantom" };

            // Act
            command.Execute(args, new Dictionary<string, string>());

            // Assert
            // Ensure no combat actions occur if characters don't exist
            mockCombatSystem.Verify(cs => cs.Attack(It.IsAny<ICombatEntity>(), It.IsAny<ICombatEntity>()), Times.Never);
        }
    }
}
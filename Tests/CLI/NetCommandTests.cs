using Xunit;
using Moq;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Core.Contracts;
using GameModel.Core.State;
using GameModel.Text;
using GameModel.Infrastructure.Network.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Optima_OOP.Tests.CLI
{
    public class NetCommandTests
    {
        [Fact]
        public void Execute_Import_ShouldAddCharacterToContext()
        {
            // Arrange
            var mockApi = new Mock<IGameDataService>();
            var worldContext = new WorldContext();
            var textFactory = new TextFactory();
            var docContext = new DocumentContext();

            // Mocking Web Response
            mockApi.Setup(s => s.GetCharacterDetailsAsync("Amber"))
                   .ReturnsAsync(new GenshinCharacterDto 
                   { 
                       Name = "Amber", 
                       Weapon = "Bow", 
                       Rarity = 4,
                       Vision = "Pyro"
                   });

            var command = new NetCommand(mockApi.Object, worldContext, textFactory, docContext);
            var args = new[] { "import", "Amber" };

            // Act
            command.Execute(args, new Dictionary<string, string>());

            // Assert
            Assert.Single(worldContext.Characters); // check character added
            Assert.Equal("Amber", worldContext.Characters[0].Name);
            // Check that warrior is created (because Bow != Catalyst)
            Assert.IsType<GameModel.Content.Characters.Warrior>(worldContext.Characters[0]);
        }
    }
}
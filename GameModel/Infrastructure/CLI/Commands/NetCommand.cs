using GameModel.Core.Contracts;
using GameModel.Core.Entities;
using GameModel.Core.State;
using GameModel.Text;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class NetCommand : ICommand
    {
        private readonly IGameDataService _apiService;
        private readonly WorldContext _worldContext;
        private readonly TextFactory _textFactory;
        private readonly DocumentContext _docContext;

        public string Keyword => "net";
        public string Description => "Usage: net <list|import|view> [char_name]. Interact with WebAPI.";

        public NetCommand(IGameDataService apiService, WorldContext worldContext, TextFactory textFactory, DocumentContext docContext)
        {
            _apiService = apiService;
            _worldContext = worldContext;
            _textFactory = textFactory;
            _docContext = docContext;
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            // Note: Since Execute is void, we run async explicitly. 
            // In a real async CLI, the interface ICommand should return Task.
            ExecuteAsync(args).GetAwaiter().GetResult();
        }

        private async Task ExecuteAsync(string[] args)
        {
            if (args.Length == 0) { Console.WriteLine(Description); return; }

            string subCommand = args[0].ToLower();

            if (subCommand == "list")
            {
                Console.WriteLine("Fetching characters from API...");
                var chars = await _apiService.GetAvailableCharactersAsync();
                Console.WriteLine($"Available: {string.Join(", ", chars.Take(10))}... (and more)");
            }
            else if (subCommand == "view" && args.Length > 1)
            {
                string name = args[1];
                var dto = await _apiService.GetCharacterDetailsAsync(name);

                if (dto != null)
                {
                    // Format and display character info using Text
                    Console.WriteLine("--- API Data Preview ---");
                    var root = new Root("Preview");
                    var heading = new Heading(dto.Name, 1, root);
                    root.AddChild(heading);

                    heading.AddChild(new Paragraph($"Rarity: {dto.Rarity} Stars"));
                    heading.AddChild(new Paragraph($"Vision: {dto.Vision}"));
                    heading.AddChild(new Paragraph($"Description: {dto.Description}"));

                    Console.WriteLine(root.Render());
                }
                else
                {
                    Console.WriteLine("Character not found.");
                }
            }
            else if (subCommand == "import" && args.Length > 1)
            {
                string name = args[1];
                var dto = await _apiService.GetCharacterDetailsAsync(name);

                if (dto != null)
                {
                    var newChar = CharacterMapper.MapToGameEntity(dto);
                    _worldContext.Characters.Add(newChar); // adding to game world
                    Console.WriteLine($"Character {newChar.Name} imported successfully into the game world!");
                }
                else
                {
                    Console.WriteLine("Character not found.");
                }
            }
        }
    }
}
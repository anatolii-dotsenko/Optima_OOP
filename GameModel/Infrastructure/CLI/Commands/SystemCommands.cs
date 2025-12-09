using GameModel.Core.Contracts;
using GameModel.Core.Data;
using GameModel.Core.Entities;
using GameModel.Core.State;
using GameModel.Infrastructure.Persistence;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class SaveCommand : ICommand
    {
        private readonly WorldContext _context;
        private readonly JsonFileRepository _repository;
        private readonly IDisplayer _displayer;

        public string Keyword => "save";
        public string Description => "Usage: save. Saves the current world state to 'savegame.json'.";

        public SaveCommand(WorldContext context, JsonFileRepository repo, IDisplayer displayer)
        {
            _context = context;
            _repository = repo;
            _displayer = displayer;
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            var saveData = new SaveBase
            {
                SaveDate = DateTime.Now,
                Characters = _context.Characters.Select(c => c.ToData()).ToList()
                // Can add item pool saving if it changes dynamically
            };

            _repository.Save(saveData);
            _displayer.WriteLine($"Game saved successfully at {saveData.SaveDate}.");
        }
    }

    public class LoadCommand : ICommand
    {
        private readonly WorldContext _context;
        private readonly JsonFileRepository _repository;
        private readonly IDisplayer _displayer;

        public string Keyword => "load";
        public string Description => "Usage: load. Loads the world state from 'savegame.json' (Simple restore).";

        public LoadCommand(WorldContext context, JsonFileRepository repo, IDisplayer displayer)
        {
            _context = context;
            _repository = repo;
            _displayer = displayer;
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            var data = _repository.Load();
            if (data == null || data.Characters.Count == 0)
            {
                _displayer.WriteLine("No save data found or save file is empty.");
                return;
            }

            // clear current world state
            _context.Characters.Clear();

            // rehydrate characters from data
            foreach (var charData in data.Characters)
            {
                var character = CharacterMapper.MapFromData(charData);
                _context.Characters.Add(character);
                _displayer.WriteLine($"restored character: {character.Name} ({character.GetType().Name})");
            }

            _displayer.WriteLine($"game state loaded successfully from {data.SaveDate}.");
        }
    }
}
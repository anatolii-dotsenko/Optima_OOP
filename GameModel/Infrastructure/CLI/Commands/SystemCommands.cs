using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Core.Contracts;
using GameModel.Core.State;
using GameModel.Core.Data;
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
                // Можна додати збереження items pool, якщо він змінюється динамічно
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

            // Note: In a full implementation, we would need a Factory to recreate specific 
            // subclasses (Mage/Warrior) based on ClassType string and restore Items.
            // For this demonstration, we will just log the data found.
            
            _displayer.WriteLine($"Found save from: {data.SaveDate}");
            _displayer.WriteLine($"Contains {data.Characters.Count} characters:");
            foreach(var charData in data.Characters)
            {
                _displayer.WriteLine($" - {charData.Name} ({charData.ClassType})");
            }
            
            _displayer.WriteLine("State loaded (Mock restore).");
        }
    }
}
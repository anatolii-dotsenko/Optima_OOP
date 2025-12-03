using System;
using System.Collections.Generic;
using System.Text;
using GameModel.Core.Contracts; // Needed for IDisplayer

namespace GameModel.Infrastructure.CLI
{
    public interface ICliSession
    {
        string ModeName { get; }
        void ExecuteCommand(string command, string[] args, Dictionary<string, string> options);
        void PrintHelp();
    }

    public class CliEngine
    {
        private readonly ICliSession _session;
        private readonly IDisplayer _displayer;

        public CliEngine(ICliSession session, IDisplayer displayer)
        {
            _session = session;
            _displayer = displayer;
        }

        public void Run()
        {
            _displayer.WriteLine($"--- Started in {_session.ModeName} mode ---");
            _displayer.WriteLine("Type 'help' for commands, 'exit' to quit.");

            while (true)
            {
                // Note: We use Console.Write here for the prompt itself usually, 
                // but let's strictly use _displayer.
                _displayer.Write($"{_session.ModeName} > ");
                
                string input = _displayer.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.Trim().ToLower() == "exit") break;

                ParseAndExecute(input);
            }
        }

        private void ParseAndExecute(string input)
        {
            var parts = ParseArguments(input);
            
            if (parts.Count == 0) return;

            string command = parts[0].ToLower();
            
            var args = new List<string>();
            var options = new Dictionary<string, string>();

            for (int i = 1; i < parts.Count; i++)
            {
                if (parts[i].StartsWith("--"))
                {
                    string key = parts[i].Substring(2);
                    string val = "true";
                    
                    if (i + 1 < parts.Count && !parts[i + 1].StartsWith("--"))
                    {
                        val = parts[i + 1];
                        i++; 
                    }
                    options[key] = val;
                }
                else
                {
                    args.Add(parts[i]);
                }
            }

            try 
            {
                if (command == "help") _session.PrintHelp();
                else _session.ExecuteCommand(command, args.ToArray(), options);
            }
            catch (Exception ex)
            {
                _displayer.WriteLine($"Error: {ex.Message}");
            }
        }

        private List<string> ParseArguments(string input)
        {
            var args = new List<string>();
            var currentArg = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in input)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ' ' && !inQuotes)
                {
                    if (currentArg.Length > 0)
                    {
                        args.Add(currentArg.ToString());
                        currentArg.Clear();
                    }
                }
                else
                {
                    currentArg.Append(c);
                }
            }

            if (currentArg.Length > 0)
            {
                args.Add(currentArg.ToString());
            }

            return args;
        }
    }
}
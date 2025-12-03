using System;
using System.Collections.Generic;

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
        private ICliSession _session;

        public CliEngine(ICliSession session)
        {
            _session = session;
        }

        public void Run()
        {
            Console.WriteLine($"--- Started in {_session.ModeName} mode ---");
            Console.WriteLine("Type 'help' for commands, 'exit' to quit.");

            while (true)
            {
                Console.Write($"{_session.ModeName} > ");
                // FIX: Console.ReadLine() returns string? (nullable)
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.Trim().ToLower() == "exit") break;

                ParseAndExecute(input);
            }
        }

        private void ParseAndExecute(string input)
        {
            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0].ToLower();
            
            var args = new List<string>();
            var options = new Dictionary<string, string>();

            for (int i = 1; i < parts.Length; i++)
            {
                if (parts[i].StartsWith("--"))
                {
                    string key = parts[i].Substring(2);
                    string val = "true";
                    
                    if (i + 1 < parts.Length && !parts[i + 1].StartsWith("--"))
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
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
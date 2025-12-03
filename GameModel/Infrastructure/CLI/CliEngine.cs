using System;
using System.Collections.Generic;
using System.Text;

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
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.Trim().ToLower() == "exit") break;

                ParseAndExecute(input);
            }
        }

        private void ParseAndExecute(string input)
        {
            // Use the smart parser instead of simple Split
            var parts = ParseArguments(input);
            
            if (parts.Count == 0) return;

            string command = parts[0].ToLower();
            
            var args = new List<string>();
            var options = new Dictionary<string, string>();

            // Iterate starting from 1 (skipping the command keyword)
            for (int i = 1; i < parts.Count; i++)
            {
                if (parts[i].StartsWith("--"))
                {
                    string key = parts[i].Substring(2);
                    string val = "true";
                    
                    // Look ahead for the value
                    if (i + 1 < parts.Count && !parts[i + 1].StartsWith("--"))
                    {
                        val = parts[i + 1];
                        i++; // Skip next part since we consumed it as a value
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

        /// <summary>
        /// Parses the input string into arguments, preserving quoted strings as single tokens.
        /// Example: 'add --id "Iron Sword"' -> ['add', '--id', 'Iron Sword']
        /// </summary>
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
                    // We don't append the quote itself to the argument
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

            // Add the last argument if exists
            if (currentArg.Length > 0)
            {
                args.Add(currentArg.ToString());
            }

            return args;
        }
    }
}
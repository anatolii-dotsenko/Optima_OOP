// handles parsing raw input strings into commands, arguments, and options
using System.Text;

namespace GameModel.Infrastructure.CLI
{
    public class ArgParser
    {
        public (string Command, string[] Args, Dictionary<string, string> Options) Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (string.Empty, Array.Empty<string>(), new Dictionary<string, string>());

            var parts = ParseArguments(input);
            if (parts.Count == 0)
                return (string.Empty, Array.Empty<string>(), new Dictionary<string, string>());

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

            return (command, args.ToArray(), options);
        }

        // helper to handle quotes logic (extracted from old CliEngine)
        private List<string> ParseArguments(string input)
        {
            var args = new List<string>();
            var currentArg = new StringBuilder();
            bool inQuotes = false;

            foreach (char c in input)
            {
                if (c == '"') inQuotes = !inQuotes;
                else if (c == ' ' && !inQuotes)
                {
                    if (currentArg.Length > 0) { args.Add(currentArg.ToString()); currentArg.Clear(); }
                }
                else currentArg.Append(c);
            }
            if (currentArg.Length > 0) args.Add(currentArg.ToString());
            return args;
        }
    }
}
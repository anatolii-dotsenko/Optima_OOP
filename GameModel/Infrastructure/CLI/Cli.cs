using GameModel.Infrastructure.CLI.Rendering;
using GameModel.Infrastructure.CLI.Strategies;

namespace GameModel.Infrastructure.CLI
{
    public class Cli
    {
        private ICommandStrategy? _currentStrategy; // Nullable to suppress warning if not set immediately
        private readonly ArgParser _argParser;
        private readonly ConsoleRenderer _renderer;

        public Cli(ConsoleRenderer renderer)
        {
            _argParser = new ArgParser();
            _renderer = renderer;
        }

        public void UseStrategy(ICommandStrategy strategy)
        {
            _currentStrategy = strategy;
            _renderer.WriteMessage($"--- Switched to: {_currentStrategy.Name} ---");
        }

        public void Display<T>(IRenderable<T> renderable)
        {
            if (_renderer is IRenderer<T> typedRenderer)
            {
                renderable.UseRenderer(typedRenderer);
            }
            else
            {
                _renderer.WriteError($"Renderer does not support type {typeof(T).Name}");
            }
        }

        public void ExecCommand(string inputLine)
        {
            if (_currentStrategy == null)
            {
                _renderer.WriteError("No strategy selected.");
                return;
            }

            var parsed = _argParser.Parse(inputLine);
            if (string.IsNullOrEmpty(parsed.Command)) return;

            if (parsed.Command == "help")
            {
                ShowHelp();
                return;
            }

            try
            {
                _currentStrategy.ExecCommand(parsed.Command, parsed.Args, parsed.Options);
            }
            catch (Exception ex)
            {
                _renderer.WriteError(ex.Message);
            }
        }

        private void ShowHelp()
        {
            if (_currentStrategy == null) return;

            _renderer.WriteMessage("Available Commands:");
            foreach (var cmd in _currentStrategy.GetAvailableCommands())
            {
                _renderer.WriteMessage($"- {cmd.Keyword}: {cmd.Description}");
            }
        }

        public void RunLoop()
        {
            while (true)
            {
                Console.Write($"{_currentStrategy?.Name ?? "CLI"} > ");

                // Fix: Handle possible null from ReadLine
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.Trim().ToLower() == "exit") break;

                // Pass non-null string
                ExecCommand(input);
            }
        }
    }
}
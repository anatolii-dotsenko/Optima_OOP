// acts as the facade for the command line interface, managing strategies and loop execution.
using GameModel.Infrastructure.CLI.Rendering;
using GameModel.Infrastructure.CLI.Strategies;

namespace GameModel.Infrastructure.CLI
{
    /// <summary>
    /// Facade for the Command Line Interface.
    /// Manages the command loop, input parsing, and strategy switching.
    /// </summary>
    public class Cli
    {
        private ICommandStrategy? _currentStrategy; // Nullable to suppress warning if not set immediately
        private readonly ArgParser _argParser;
        private readonly IConsoleRenderer _renderer; // Refactored to Interface (DIP)

        /// <summary>
        /// Initializes a new instance of the CLI facade.
        /// </summary>
        /// <param name="renderer">The console renderer implementation.</param>
        public Cli(IConsoleRenderer renderer)
        {
            _argParser = new ArgParser();
            _renderer = renderer;
        }

        /// <summary>
        /// Switches the active command strategy (e.g., from RPG mode to Text Editor mode).
        /// </summary>
        public void UseStrategy(ICommandStrategy strategy)
        {
            _currentStrategy = strategy;
            _renderer.WriteMessage($"--- Switched to: {_currentStrategy.Name} ---");
        }

        /// <summary>
        /// Displays a renderable object using the configured renderer if supported.
        /// </summary>
        public void Display<T>(IRenderable<T> renderable)
        {
            // Dynamic check to see if the renderer supports the specific data type
            if (_renderer is IRenderer<T> typedRenderer)
            {
                renderable.UseRenderer(typedRenderer);
            }
            else
            {
                _renderer.WriteError($"Renderer does not support type {typeof(T).Name}");
            }
        }

        /// <summary>
        /// Parses and executes a single line of input.
        /// </summary>
        public void ExecCommand(string inputLine)
        {
            if (_currentStrategy == null)
            {
                _renderer.WriteError("No strategy selected.");
                return;
            }

            var parsed = _argParser.Parse(inputLine);
            if (string.IsNullOrEmpty(parsed.Command)) return;

            // Global interception for 'help' command
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

        /// <summary>
        /// Starts the main application loop, blocking until exit.
        /// </summary>
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
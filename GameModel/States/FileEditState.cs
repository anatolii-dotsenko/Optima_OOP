using GameModel.Core.Contracts;
using GameModel.Infrastructure.CLI;
using GameModel.Infrastructure.CLI.Commands;
using GameModel.Text;

namespace GameModel.States
{
    public class FileEditState : IAppState
    {
        private readonly string _fileName;
        private readonly DocumentContext _docContext;
        private readonly CommandRegistry _registry;
        private readonly TextFactory _factory;

        public string Name => "EDIT";

        public FileEditState(string fileName, string initialContent)
        {
            _fileName = fileName;
            _docContext = new DocumentContext();
            _factory = new TextFactory();

            // Parse initial content
            var lines = initialContent.Split('\n');
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    _factory.AddElement(_docContext, TextType.Paragraph, line.Trim());
            }

            _registry = new CommandRegistry();
            _registry.Register(new AddTextCommand(_docContext, _factory));
            _registry.Register(new PrintCommand(_docContext));
            _registry.Register(new RmCommand(_docContext));
            _registry.Register(new HelpCommand(_registry));
        }

        public void Render(IDisplayer displayer)
        {
            // Optional: print document preview
        }

        public void HandleInput(string input, FileManagerApp context)
        {
            var parts = input.Split(' ');
            string cmdKey = parts[0].ToLower();

            if (cmdKey == "save")
            {
                string content = _docContext.Root.Render();
                context.FileSystem.WriteAllText(_fileName, content);
                Console.WriteLine("File saved.");
                return;
            }
            if (cmdKey == "exit_editor")
            {
                string content = _docContext.Root.Render();
                context.TransitionTo(new FileViewState(_fileName, content));
                return;
            }

            var cmd = _registry.GetCommand(cmdKey);
            if (cmd != null)
            {
                var args = new List<string>(parts);
                args.RemoveAt(0);
                cmd.Execute(args.ToArray(), new Dictionary<string, string>());
            }
            else
            {
                Console.WriteLine("Unknown edit command. Try 'add', 'print', 'rm', 'save', or 'exit_editor'.");
            }
        }
    }
}
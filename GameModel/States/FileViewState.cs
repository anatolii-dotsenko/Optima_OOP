using GameModel.Core.Contracts;

namespace GameModel.States
{
    public class FileViewState : IAppState
    {
        private readonly string _fileName;
        private readonly string _content;

        public string Name => "VIEW";

        public FileViewState(string fileName, string content)
        {
            _fileName = fileName;
            _content = content;
        }

        public void Render(IDisplayer displayer)
        {
            displayer.WriteLine($"--- Viewing: {_fileName} ---");
            displayer.WriteLine(_content);
            displayer.WriteLine("-----------------------------");
            displayer.WriteLine("Commands: edit, close");
        }

        public void HandleInput(string input, FileManagerApp context)
        {
            if (input == "close")
            {
                context.TransitionTo(new DirectoryState());
            }
            else if (input == "edit")
            {
                // Requires FileEditState to be created
                context.TransitionTo(new FileEditState(_fileName, _content));
            }
        }
    }
}
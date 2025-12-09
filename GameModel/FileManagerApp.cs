using GameModel.Core.Contracts;

namespace GameModel
{
    public class FileManagerApp
    {
        private IAppState _currentState;
        private readonly IDisplayer _displayer;
        public IFileSystem FileSystem { get; }

        public FileManagerApp(IFileSystem fileSystem, IDisplayer displayer, IAppState initialState)
        {
            FileSystem = fileSystem;
            _displayer = displayer;
            _currentState = initialState;
        }

        public void TransitionTo(IAppState newState)
        {
            _currentState = newState;
            _displayer.Clear();
        }

        public void Run()
        {
            while (true)
            {
                // 1. Render current state
                _currentState.Render(_displayer);

                // 2. Get Input
                _displayer.Write($"{_currentState.Name} > ");
                string input = _displayer.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.Trim().ToLower() == "exit") break;

                // 3. Handle Input
                _currentState.HandleInput(input, this);
            }
        }
    }
}
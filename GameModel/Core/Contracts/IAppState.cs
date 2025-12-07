namespace GameModel.Core.Contracts
{
    public interface IAppState
    {
        string Name { get; }
        void Render(IDisplayer displayer);
        void HandleInput(string input, FileManagerApp context);
    }
}
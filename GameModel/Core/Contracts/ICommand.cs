namespace GameModel.Core.Contracts
{
    public interface ICommand
    {
        string Keyword { get; }
        string Description { get; }
        void Execute(string[] args);
    }
}
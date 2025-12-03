namespace GameModel.Core.Contracts
{
    public interface IDisplayer
    {
        void Write(string message);
        void WriteLine(string message);
        string ReadLine();
        void Clear();
    }
}
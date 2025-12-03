using System.Collections.Generic;

namespace GameModel.Core.Contracts
{
    public interface ICommand
    {
        string Keyword { get; }
        string Description { get; }
        // Updated to support options (e.g., --id 5)
        void Execute(string[] args, Dictionary<string, string> options);
    }
}
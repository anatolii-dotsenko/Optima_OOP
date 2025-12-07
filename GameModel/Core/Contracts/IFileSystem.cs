using System.Collections.Generic;

namespace GameModel.Core.Contracts
{
    public interface IFileSystem
    {
        string GetCurrentDirectory();
        void ChangeDirectory(string path);
        IEnumerable<string> GetFiles(string path);
        IEnumerable<string> GetDirectories(string path);
        string ReadAllText(string path);
        void WriteAllText(string path, string content);
        bool Exists(string path);
    }
}
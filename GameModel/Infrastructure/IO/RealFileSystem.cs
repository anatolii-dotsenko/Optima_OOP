using System.IO;
using System.Collections.Generic;
using GameModel.Core.Contracts;

namespace GameModel.Infrastructure.IO
{
    public class RealFileSystem : IFileSystem
    {
        private string _currentPath = Directory.GetCurrentDirectory();

        public string GetCurrentDirectory() => _currentPath;

        public void ChangeDirectory(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            string newPath = Path.GetFullPath(Path.Combine(_currentPath, path));
            
            if (Directory.Exists(newPath))
            {
                _currentPath = newPath;
            }
            else
            {
                throw new DirectoryNotFoundException($"Directory '{newPath}' not found");
            }
        }

        public IEnumerable<string> GetFiles(string path) => Directory.GetFiles(path);
        public IEnumerable<string> GetDirectories(string path) => Directory.GetDirectories(path);
        public string ReadAllText(string path) => File.ReadAllText(Path.Combine(_currentPath, path));
        public void WriteAllText(string path, string content) => File.WriteAllText(Path.Combine(_currentPath, path), content);
        public bool Exists(string path) => File.Exists(Path.Combine(_currentPath, path));
    }
}
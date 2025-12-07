using System;
using System.IO;
using System.Linq;
using GameModel.Core.Contracts;

namespace GameModel.States
{
    public class DirectoryState : IAppState
    {
        public string Name => "DIR";

        public void Render(IDisplayer displayer) 
        { 
            displayer.WriteLine("--- Directory Mode ---");
            displayer.WriteLine("Commands: ls, cd <path>, open <file>, exit");
        }

        public void HandleInput(string input, FileManagerApp context)
        {
            var parts = input.Split(' ');
            var cmd = parts[0].ToLower();
            var arg = parts.Length > 1 ? parts[1] : "";

            try
            {
                switch (cmd)
                {
                    case "ls":
                        var path = context.FileSystem.GetCurrentDirectory();
                        var dirs = context.FileSystem.GetDirectories(path);
                        var files = context.FileSystem.GetFiles(path);
                        
                        foreach (var d in dirs)
                            System.Console.WriteLine($"[DIR] {Path.GetFileName(d)}");
                        foreach (var f in files)
                            System.Console.WriteLine($"      {Path.GetFileName(f)}");
                        break;

                    case "cd":
                        context.FileSystem.ChangeDirectory(arg);
                        System.Console.WriteLine($"Changed to: {context.FileSystem.GetCurrentDirectory()}");
                        break;

                    case "open":
                        if (context.FileSystem.Exists(arg))
                        {
                            var content = context.FileSystem.ReadAllText(arg);
                            context.TransitionTo(new FileViewState(arg, content));
                        }
                        else 
                        {
                            System.Console.WriteLine("File not found.");
                        }
                        break;

                    default:
                        System.Console.WriteLine("Unknown command.");
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
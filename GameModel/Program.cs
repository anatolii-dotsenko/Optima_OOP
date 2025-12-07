using System;
using GameModel.Infrastructure.IO;
using GameModel.Infrastructure.Setup;
using GameModel.States;

namespace GameModel
{
    class Program
    {
static void Main(string[] args)
{
    // Check args to switch between Game and File Manager
    if (args.Length > 0 && args[0] == "--file-manager")
    {
        var fs = new RealFileSystem();
        var displayer = new ConsoleDisplayer();
        var app = new FileManagerApp(fs, displayer, new DirectoryState());
        app.Run();
    }
    else 
    {
        var builder = new GameBuilder();
        var cli = builder.Build(args); // Changed from BuildEngine to Build
        cli.RunLoop();
    }
}        
    }
}
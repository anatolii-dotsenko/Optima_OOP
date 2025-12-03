using System;
using GameModel.Infrastructure.Setup;

namespace GameModel
{
    class Program
    {
        static void Main(string[] args)
        {
            // The composition root is now cleaner.
            // All wiring logic is hidden inside GameBuilder.BuildEngine.
            
            var builder = new GameBuilder();
            var engine = builder.BuildEngine(args);
            
            engine.Run();
        }
    }
}
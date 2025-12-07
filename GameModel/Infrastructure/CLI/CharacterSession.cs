// using System;
// using System.Collections.Generic;

// namespace GameModel.Infrastructure.CLI
// {
//     public class CharacterSession : ICliSession
//     {
//         public string ModeName => "Characters";
//         private readonly CommandRegistry _registry;

//         public CharacterSession(CommandRegistry registry)
//         {
//             _registry = registry;
//         }

//         public void PrintHelp()
//         {
//             var cmd = _registry.GetCommand("help");
//             cmd?.Execute(Array.Empty<string>(), new Dictionary<string, string>());
//         }

//         public void ExecuteCommand(string command, string[] args, Dictionary<string, string> options)
//         {
//             var cmdObj = _registry.GetCommand(command);
//             if (cmdObj != null)
//             {
//                 cmdObj.Execute(args, options);
//             }
//             else
//             {
//                 Console.WriteLine("Unknown command. Type 'help' to see available commands.");
//             }
//         }
//     }
// }
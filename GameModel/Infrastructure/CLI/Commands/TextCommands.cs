using System;
using System.Collections.Generic;
using GameModel.Core.Contracts;
using GameModel.Text;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class AddTextCommand : ICommand
    {
        private readonly DocumentContext _context;
        private readonly TextFactory _factory;
        public string Keyword => "add";
        public string Description => "Usage: add <container|leaf> <content>";

        public AddTextCommand(DocumentContext context, TextFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (args.Length < 2) { Console.WriteLine(Description); return; }
            
            string typeStr = args[0].ToLower();
            string content = args[1];
            
            TextType type = (typeStr == "container" || typeStr == "heading") ? TextType.Heading : TextType.Paragraph;
            _factory.AddElement(_context, type, content);
            
            Console.WriteLine($"Added {type}.");
        }
    }

    public class PwdCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "pwd";
        public string Description => "Prints current path";

        public PwdCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            var path = new List<string>();
            var curr = (IText)_context.CurrentContainer;
            while (curr != null)
            {
                path.Add(curr.Name);
                curr = curr.Parent;
            }
            path.Reverse();
            Console.WriteLine("/" + string.Join("/", path));
        }
    }

    public class PrintCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "print";
        public string Description => "Prints content. Use --whole for full doc.";

        public PrintCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            IText target = options.ContainsKey("whole") ? _context.Root : _context.CurrentContainer;
            Console.WriteLine(target.Render());
        }
    }

    public class ChangeDirCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "cd";
        public string Description => "Usage: cd <name> or cd ..";

        public ChangeDirCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (args.Length == 0) return;
            string name = args[0];

            if (name == "..")
            {
                if (_context.CurrentContainer.Parent != null)
                {
                    _context.CurrentContainer = _context.CurrentContainer.Parent;
                    Console.WriteLine("Moved up.");
                }
                else Console.WriteLine("At root.");
                return;
            }

            var child = _context.CurrentContainer.FindChild(name);
            if (child is Container c)
            {
                _context.CurrentContainer = c;
                Console.WriteLine($"Changed to {c.Name}");
            }
            else
            {
                Console.WriteLine("Directory not found.");
            }
        }
    }
}
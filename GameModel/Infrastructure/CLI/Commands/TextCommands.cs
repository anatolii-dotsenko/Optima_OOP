using System;
using System.Collections.Generic;
using System.Linq;
using GameModel.Core.Contracts;
using GameModel.Text;

namespace GameModel.Infrastructure.CLI.Commands
{
    public class AddTextCommand : ICommand
    {
        private readonly DocumentContext _context;
        private readonly TextFactory _factory;
        public string Keyword => "add";
        
        // Updated description to reflect simplified usage
        public string Description => "Usage: add <heading|paragraph>";

        public AddTextCommand(DocumentContext context, TextFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            // Requires only 1 argument now (the type)
            if (args.Length < 1) { Console.WriteLine(Description); return; }
            
            string typeArg = args[0].ToLower(); // e.g. heading, paragraph

            // Determine the TextType directly from the argument
            TextType type;
            if (typeArg == "heading")
            {
                type = TextType.Heading;
            }
            else if (typeArg == "paragraph")
            {
                type = TextType.Paragraph;
            }
            else
            {
                Console.WriteLine("Unknown type. Available types: heading, paragraph");
                return;
            }

            // Dialog for content remains the same
            Console.Write("Enter name/content: ");
            string content = Console.ReadLine() ?? "";

            _factory.AddElement(_context, type, content);
            
            Console.WriteLine($"Added {type} '{content}'.");
        }
    }

    public class PrintCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "print";
        public string Description => "Usage: print [--whole | --id]";

        public PrintCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            bool whole = options.ContainsKey("whole");
            bool showId = options.ContainsKey("id");

            IText target = whole ? _context.Root : _context.CurrentContainer;
            
            Console.WriteLine($"--- Content of {(whole ? "Document" : target.Name)} ---");
            if (showId) Console.WriteLine($"[ID: {target.Id}]");
            
            Console.WriteLine(target.Render());
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

    public class UpCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "up";
        public string Description => "Moves to parent container";

        public UpCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (_context.CurrentContainer.Parent != null)
            {
                _context.CurrentContainer = _context.CurrentContainer.Parent;
                Console.WriteLine($"Moved up to '{_context.CurrentContainer.Name}'");
            }
            else
            {
                Console.WriteLine("Already at Root.");
            }
        }
    }

    public class RmCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "rm";
        public string Description => "Usage: rm [name]";

        public RmCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            // Case 1: No args -> Remove current and move up
            if (args.Length == 0)
            {
                Console.Write($"Delete current container '{_context.CurrentContainer.Name}'? (y/n): ");
                string? ans = Console.ReadLine();
                if (ans?.ToLower() == "y")
                {
                    if (_context.CurrentContainer.Parent == null) 
                    { 
                        Console.WriteLine("Cannot remove Root."); 
                        return; 
                    }
                    
                    var parent = _context.CurrentContainer.Parent;
                    parent.RemoveChild(_context.CurrentContainer.Name); 
                    _context.CurrentContainer = parent;
                    Console.WriteLine("Removed current. Moved up.");
                }
            }
            // Case 2: Remove child by name
            else
            {
                string target = args[0];
                if (_context.CurrentContainer.RemoveChild(target)) 
                    Console.WriteLine($"Child '{target}' removed.");
                else 
                    Console.WriteLine("Child not found.");
            }
        }
    }

    public class ChangeDirCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "cd";
        public string Description => "Usage: cd <path> | [--id <id>]";

        public ChangeDirCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            // Priority to --id
            if (options.TryGetValue("id", out string? idVal) && idVal != null)
            {
                var found = _context.CurrentContainer.FindChild(idVal);
                if (found is Container c)
                {
                    _context.CurrentContainer = c;
                    Console.WriteLine($"Changed to container {c.Name} (by ID).");
                }
                else Console.WriteLine("Container not found by ID.");
                return;
            }

            // Path navigation
            if (args.Length > 0)
            {
                string path = args[0];
                var found = _context.CurrentContainer.FindChild(path);
                if (found is Container c)
                {
                    _context.CurrentContainer = c;
                    Console.WriteLine($"Changed to '{c.Name}'");
                }
                else Console.WriteLine("Path not found.");
            }
        }
    }
}
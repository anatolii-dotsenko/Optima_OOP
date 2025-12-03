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
        public string Description => "Usage: add <heading|paragraph>. Interactive dialog to add a new text element to the current container.";

        public AddTextCommand(DocumentContext context, TextFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (args.Length < 1) { Console.WriteLine(Description); return; }
            
            string typeArg = args[0].ToLower(); 

            TextType type;
            if (typeArg == "heading") type = TextType.Heading;
            else if (typeArg == "paragraph") type = TextType.Paragraph;
            else
            {
                Console.WriteLine("Unknown text type. Please use 'heading' or 'paragraph'.");
                return;
            }

            Console.Write($"Enter content for new {type}: ");
            string content = Console.ReadLine() ?? "";

            _factory.AddElement(_context, type, content);
            
            Console.WriteLine($"Added {type} '{content}'.");
        }
    }

    public class PrintCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "print";
        public string Description => "Usage: print [--whole | --id]. Displays current element. --whole shows entire doc, --id shows IDs.";

        public PrintCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            bool whole = options.ContainsKey("whole");
            bool showId = options.ContainsKey("id");

            IText target = whole ? _context.Root : _context.CurrentContainer;
            
            Console.WriteLine($"--- Content of {(whole ? "Whole Document" : target.Name)} ---");
            if (showId) Console.WriteLine($"[ID: {target.Id}]");
            
            Console.WriteLine(target.Render());
        }
    }

    public class PwdCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "pwd";
        public string Description => "Usage: pwd. Prints the absolute path of the current container.";

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
        public string Description => "Usage: up. Navigates up to the parent container.";

        public UpCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            if (_context.CurrentContainer.Parent != null)
            {
                _context.CurrentContainer = _context.CurrentContainer.Parent;
                Console.WriteLine($"Moved up to '{_context.CurrentContainer.Name}'.");
            }
            else
            {
                Console.WriteLine("Already at Root level. Cannot move up.");
            }
        }
    }

    public class RmCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "rm";
        public string Description => "Usage: rm [name]. Removes a child by name, or removes the current container (with confirmation).";

        public RmCommand(DocumentContext context) => _context = context;

        public void Execute(string[] args, Dictionary<string, string> options)
        {
            // Case 1: No args -> Remove current and move up
            if (args.Length == 0)
            {
                Console.Write($"Are you sure you want to delete the current container '{_context.CurrentContainer.Name}'? (y/n): ");
                string? ans = Console.ReadLine();
                if (ans?.ToLower() == "y")
                {
                    if (_context.CurrentContainer.Parent == null) 
                    { 
                        Console.WriteLine("Cannot remove the Root container."); 
                        return; 
                    }
                    
                    var parent = _context.CurrentContainer.Parent;
                    parent.RemoveChild(_context.CurrentContainer.Name); 
                    _context.CurrentContainer = parent;
                    Console.WriteLine("Current container removed. Moved up to parent.");
                }
                else
                {
                    Console.WriteLine("Operation cancelled.");
                }
            }
            // Case 2: Remove child by name
            else
            {
                string target = args[0];
                if (_context.CurrentContainer.RemoveChild(target)) 
                    Console.WriteLine($"Child element '{target}' removed successfully.");
                else 
                    Console.WriteLine($"Child '{target}' not found in current container.");
            }
        }
    }

    public class ChangeDirCommand : ICommand
    {
        private readonly DocumentContext _context;
        public string Keyword => "cd";
        public string Description => "Usage: cd <path> | [--id <container_id>]. Navigates to a container by name/path or ID.";

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
                    Console.WriteLine($"Changed directory to '{c.Name}' (found by ID).");
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
                    Console.WriteLine($"Changed directory to '{c.Name}'.");
                }
                else Console.WriteLine($"Path '{path}' not found.");
            }
            else
            {
                Console.WriteLine(Description);
            }
        }
    }
}
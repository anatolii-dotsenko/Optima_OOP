using System;
using System.Collections.Generic;
using GameModel.Text; // Assuming Text classes are still in GameModel/Text

namespace GameModel.Infrastructure.CLI
{
    public class TextSession : ICliSession
    {
        public string ModeName => "Text";
        
        private Root _root;
        private Container _currentContainer;

        public TextSession()
        {
            _root = new Root("DocumentRoot");
            _currentContainer = _root;
        }

        public void PrintHelp()
        {
            Console.WriteLine("Commands: pwd, print, add, rm, up, cd");
        }

        public void ExecuteCommand(string command, string[] args, Dictionary<string, string> options)
        {
            switch (command)
            {
                case "pwd": PrintWorkingDirectory(); break;
                case "print": PrintElement(options); break;
                case "add": AddElement(args); break;
                case "rm": RemoveElement(args); break;
                case "up": NavigateUp(); break;
                case "cd": ChangeDirectory(args, options); break;
                case "ls": PrintChildren(); break;
                default: Console.WriteLine("Unknown command."); break;
            }
        }

        private void PrintWorkingDirectory()
        {
            var path = new List<string>();
            var curr = (IText)_currentContainer;
            while (curr != null)
            {
                path.Add(curr.Name);
                curr = curr.Parent;
            }
            path.Reverse();
            Console.WriteLine("/" + string.Join("/", path));
        }

        private void PrintElement(Dictionary<string, string> options)
        {
            bool whole = options.ContainsKey("whole");
            bool showId = options.ContainsKey("id");
            IText target = whole ? _root : _currentContainer;
            
            Console.WriteLine($"--- Content of '{target.Name}' ---");
            if (showId) Console.WriteLine($"ID: {target.Id}");
            Console.WriteLine(target.Render());
        }

        private void PrintChildren()
        {
            Console.WriteLine($"Listing children of '{_currentContainer.Name}':");
            foreach (var child in _currentContainer.GetChildren())
            {
                Console.WriteLine($" - [{child.GetType().Name}] {child.Name} (ID: {child.Id})");
            }
        }

        private void AddElement(string[] args)
        {
            string type = args.Length > 0 ? args[0] : Ask("Type (heading/paragraph):");
            string name = args.Length > 1 ? args[1] : Ask("Name/Content:");

            if (type == "heading")
            {
                _currentContainer.AddChild(new Heading(name, 1, _currentContainer));
            }
            else if (type == "paragraph" || type == "leaf")
            {
                _currentContainer.AddChild(new Paragraph(name, name));
            }
            else
            {
                Console.WriteLine("Unknown type.");
                return;
            }
            Console.WriteLine("Element added.");
        }

        private void RemoveElement(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Write($"Remove current container '{_currentContainer.Name}'? (y/n): ");
                if (Console.ReadLine() == "y")
                {
                    if (_currentContainer.Parent == null) { Console.WriteLine("Cannot remove Root."); return; }
                    var parent = _currentContainer.Parent;
                    parent.RemoveChild(_currentContainer.Id.ToString());
                    _currentContainer = parent;
                    Console.WriteLine("Removed current. Moved up.");
                }
            }
            else
            {
                string target = args[0];
                if (_currentContainer.RemoveChild(target)) Console.WriteLine("Child removed.");
                else Console.WriteLine("Child not found.");
            }
        }

        private void NavigateUp()
        {
            if (_currentContainer.Parent != null)
            {
                _currentContainer = _currentContainer.Parent;
                Console.WriteLine($"Moved up to '{_currentContainer.Name}'");
            }
            else Console.WriteLine("Already at Root.");
        }

        private void ChangeDirectory(string[] args, Dictionary<string, string> options)
        {
            string target = options.ContainsKey("id") ? options["id"] : (args.Length > 0 ? args[0] : null);
            if (target == null) return;

            var found = _currentContainer.FindChild(target);
            if (found is Container c)
            {
                _currentContainer = c;
                Console.WriteLine($"Changed context to '{c.Name}'");
            }
            else Console.WriteLine(found != null ? "Not a container." : "Not found.");
        }

        private string Ask(string q)
        {
            Console.Write(q + " ");
            return Console.ReadLine() ?? "";
        }
    }
}
using System;

namespace GameModel.Text
{
    public class TextFactory
    {
        private Container _current;

        public TextFactory(string title)
        {
            _current = new Root(title);
        }

        public void AddHeading(string name, int rank = 0)
        {
            var heading = new Heading(name, rank, _current);
            _current.AddChild(heading);
            _current = heading;
        }

        public void AddParagraph(string content)
        {
            _current.AddChild(new Paragraph(content));
        }

        public void Up()
        {
            if (_current.Parent == null)
                Console.WriteLine("Already at root.");
            else
                _current = _current.Parent;
        }

        public override string ToString()
        {
            var root = _current;
            while (root.Parent != null)
                root = root.Parent;

            return root.Render();
        }
    }
}
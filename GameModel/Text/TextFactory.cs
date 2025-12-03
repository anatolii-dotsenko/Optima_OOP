using System;

namespace GameModel.Text
{
    /// <summary>
    /// Provides a fluent-like interface to build the text document structure.
    /// Maintains a pointer to the current active container context.
    /// </summary>
    public class TextFactory
    {
        private Container _current;

        /// <summary>
        /// Initializes the factory with a root document.
        /// </summary>
        /// <param name="title">The title of the root document.</param>
        public TextFactory(string title)
        {
            _current = new Root(title);
        }

        /// <summary>
        /// Adds a new heading and makes it the active container.
        /// Future additions will be added as children of this heading until Up() is called.
        /// </summary>
        /// <param name="name">The heading text.</param>
        /// <param name="rank">The visual indentation level.</param>
        public void AddHeading(string name, int rank = 0)
        {
            var heading = new Heading(name, rank, _current);
            _current.AddChild(heading);
            
            // Dive into the new heading, making it the current context
            _current = heading;
        }

        /// <summary>
        /// Adds a paragraph to the currently active container.
        /// Does not change the active container.
        /// </summary>
        /// <param name="content">The paragraph text.</param>
        public void AddParagraph(string content)
        {
            _current.AddChild(new Paragraph(content));
        }

        /// <summary>
        /// Moves the active context up to the parent container.
        /// Effectively "closes" the current section.
        /// </summary>
        public void Up()
        {
            if (_current.Parent == null)
                Console.WriteLine("Already at root. Cannot move up.");
            else
                _current = _current.Parent;
        }

        /// <summary>
        /// Renders the entire document tree starting from the root.
        /// </summary>
        /// <returns>The complete formatted document string.</returns>
        public override string ToString()
        {
            // Traverse up to find the root before rendering
            var root = _current;
            while (root.Parent != null)
                root = root.Parent;

            return root.Render();
        }
    }
}
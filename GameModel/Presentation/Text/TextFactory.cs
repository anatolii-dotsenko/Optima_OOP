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
        /// Move the current context up to the parent.
        /// Returns true when moved successfully, false if already at root.
        /// Does not perform any I/O so callers control logging/handling.
        /// </summary>
        public bool Up()
        {
            if (_current.Parent == null)
            {
                return false;
            }

            _current = _current.Parent;
            return true;
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
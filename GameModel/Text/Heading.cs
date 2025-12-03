using System.Text;

namespace GameModel.Text
{
    /// <summary>
    /// Represents a section heading that organizes content hierarchically.
    /// Indentation increases based on the nesting rank.
    /// </summary>
    public class Heading : Container
    {
        private readonly int _rank;

        /// <summary>
        /// Initializes a new instance of the <see cref="Heading"/> class.
        /// </summary>
        /// <param name="name">The text of the heading.</param>
        /// <param name="rank">The depth level (0 for top-level, 1 for nested, etc.).</param>
        /// <param name="parent">The parent container.</param>
        public Heading(string name, int rank, Container parent)
            : base(name, parent)
        {
            _rank = rank;
        }

        /// <inheritdoc />
        public override string Render()
        {
            var sb = new StringBuilder();
            
            // Render the heading itself with indentation based on rank
            sb.AppendLine(new string('\t', _rank) + _name);

            // Recursively render all children, indented one step further
            foreach (var child in _children)
                sb.AppendLine(new string('\t', _rank + 1) + child.Render());

            return sb.ToString();
        }
    }
}
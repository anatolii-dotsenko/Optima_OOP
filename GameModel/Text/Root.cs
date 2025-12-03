using System.Text;

namespace GameModel.Text
{
    /// <summary>
    /// Represents the top-level document container.
    /// </summary>
    public class Root : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Root"/> class.
        /// </summary>
        /// <param name="name">The title of the document.</param>
        public Root(string name) : base(name, null) {}

        /// <summary>
        /// Checks if this node is the root of the tree.
        /// </summary>
        public bool IsRoot() => Parent == null;

        /// <inheritdoc />
        public override string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_name); // Render document title

            // Render all top-level children
            foreach (var child in _children)
                sb.AppendLine(child.Render());

            return sb.ToString();
        }
    }
}
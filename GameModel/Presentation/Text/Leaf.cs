namespace GameModel.Text
{
    /// <summary>
    /// Represents a terminal text node that cannot have children (e.g., a simple paragraph).
    /// </summary>
    public abstract class Leaf : IText
    {
        /// <summary>
        /// The actual text content.
        /// </summary>
        protected string? _content;

        /// <summary>
        /// Initializes a new instance of the <see cref="Leaf"/> class.
        /// </summary>
        /// <param name="content">The raw text content.</param>
        protected Leaf(string content)
        {
            _content = content;
        }

        /// <summary>
        /// Returns the content of the leaf node.
        /// </summary>
        /// <returns>The stored text content.</returns>
        public abstract string Render();
    }
}
namespace GameModel.Text
{
    /// <summary>
    /// Represents a standard block of text.
    /// </summary>
    public class Paragraph : Leaf
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paragraph"/> class.
        /// </summary>
        /// <param name="content">The text content of the paragraph.</param>
        public Paragraph(string content) : base(content) {}

        /// <inheritdoc />
        public override string Render()
        {
            return _content ?? string.Empty;
        }
    }
}
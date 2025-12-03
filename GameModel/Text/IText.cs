namespace GameModel.Text
{
    /// <summary>
    /// Defines the contract for any text element in the document structure.
    /// Implements the Component role in the Composite design pattern.
    /// </summary>
    public interface IText
    {
        /// <summary>
        /// Generates the string representation of the text element.
        /// </summary>
        /// <returns>The formatted string content.</returns>
        string Render();
    }
}
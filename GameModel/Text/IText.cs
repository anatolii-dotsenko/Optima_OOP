namespace GameModel.Text
{
    /// <summary>
    /// Interface for any text element.
    /// Added Id and Name to support CLI commands.
    /// </summary>
    public interface IText
    {
        Guid Id { get; }
        string Name { get; }

        /// <summary>
        /// Reference to parent container for the 'up' command.
        /// </summary>
        Container? Parent { get; set; }

        string Render();
    }
}
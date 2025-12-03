namespace GameModel.Text
{
    /// <summary>
    /// Holds the shared state for the Text Editor (The document tree and cursor).
    /// </summary>
    public class DocumentContext
    {
        public Root Root { get; }
        public Container CurrentContainer { get; set; }

        public DocumentContext()
        {
            Root = new Root("DocumentRoot");
            CurrentContainer = Root;
        }
    }
}
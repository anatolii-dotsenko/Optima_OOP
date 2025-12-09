namespace GameModel.Text
{
    /// <summary>
    /// Base class to avoid code duplication for ID and Name.
    /// </summary>
    public abstract class TextBase : IText
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; protected set; }
        public Container? Parent { get; set; }

        protected TextBase(string name)
        {
            Name = name;
        }

        public abstract string Render();
    }
}
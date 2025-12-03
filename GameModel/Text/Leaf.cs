namespace GameModel.Text
{
    public abstract class Leaf : IText
    {
        protected string? _content;

        protected Leaf(string content)
        {
            _content = content;
        }

        public abstract string Render();
    }
}
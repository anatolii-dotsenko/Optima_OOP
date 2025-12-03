namespace GameModel.Text
{
    public class Paragraph : TextBase
    {
        private string _content;

        public Paragraph(string content) : base("paragraph") // Default name for the leaf
        {
            _content = content;
        }

        // Overload for named paragraphs, if needed
        public Paragraph(string name, string content) : base(name)
        {
            _content = content;
        }

        public override string Render() => _content;
    }
}
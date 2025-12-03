namespace GameModel.Text
{
    public class Paragraph : TextBase
    {
        private string _content;

        public Paragraph(string content) : base("paragraph") // Ім'я за замовчуванням для ліста
        {
            _content = content;
        }

        // Перевантаження для іменованих параграфів, якщо треба
        public Paragraph(string name, string content) : base(name)
        {
            _content = content;
        }

        public override string Render() => _content;
    }
}
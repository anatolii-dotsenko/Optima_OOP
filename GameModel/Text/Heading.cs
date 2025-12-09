using System.Text;

namespace GameModel.Text
{
    public class Heading : Container
    {
        private readonly int _rank;

        public Heading(string name, int rank, Container? parent) : base(name, parent)
        {
            _rank = rank;
        }

        public override string Render()
        {
            var sb = new StringBuilder();
            string indent = new string('#', _rank + 1);
            sb.AppendLine($"{indent} {Name}");

            foreach (var child in _children)
                sb.AppendLine(child.Render());

            return sb.ToString();
        }
    }
}
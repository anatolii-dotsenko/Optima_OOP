using System.Text;

namespace GameModel.Text
{
    public class Heading : Container
    {
        private readonly int _rank;

        public Heading(string name, int rank, Container parent)
            : base(name, parent)
        {
            _rank = rank;
        }

        public override string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string('\t', _rank) + _name);

            foreach (var child in _children)
                sb.AppendLine(new string('\t', _rank + 1) + child.Render());

            return sb.ToString();
        }
    }
}
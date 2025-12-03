using System.Text;

namespace GameModel.Text
{
    public class Root : Container
    {
        public Root(string name) : base(name, null) {}

        public bool IsRoot() => Parent == null;

        public override string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine(_name);

            foreach (var child in _children)
                sb.AppendLine(child.Render());

            return sb.ToString();
        }
    }
}
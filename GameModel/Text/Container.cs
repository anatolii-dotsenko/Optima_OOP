using System.Text;

namespace GameModel.Text
{
    public class Container : TextBase
    {
        protected List<IText> _children = new();

        public Container(string name, Container? parent = null) : base(name)
        {
            Parent = parent;
        }

        public void AddChild(IText child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        public bool RemoveChild(string nameOrId)
        {
            var child = FindChild(nameOrId);
            if (child != null)
            {
                _children.Remove(child);
                return true;
            }
            return false;
        }

        public IText? FindChild(string nameOrId)
        {
            // Try to find by ID
            if (Guid.TryParse(nameOrId, out Guid guid))
            {
                return _children.FirstOrDefault(c => c.Id == guid);
            }
            // Or by name (case-insensitive)
            return _children.FirstOrDefault(c => c.Name.Equals(nameOrId, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<IText> GetChildren() => _children;

        public override string Render()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Name)) sb.AppendLine($"[{Name}]");
            foreach (var child in _children)
            {
                sb.AppendLine(child.Render());
            }
            return sb.ToString();
        }
    }
}
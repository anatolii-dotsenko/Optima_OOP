using System.Collections.Generic;

namespace GameModel.Text
{
    public abstract class Container : IText
    {
        protected string _name;
        protected List<IText> _children = new();
        public Container? Parent { get; }

        protected Container(string name, Container? parent = null)
        {
            _name = name;
            Parent = parent;
        }

        public void AddChild(IText child)
        {
            _children.Add(child);
        }

        public abstract string Render();
    }
}
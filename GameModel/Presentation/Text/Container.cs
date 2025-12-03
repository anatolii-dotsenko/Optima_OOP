using System.Collections.Generic;

namespace GameModel.Text
{
    /// <summary>
    /// Represents a composite text element that can contain other text elements (children).
    /// </summary>
    public abstract class Container : IText
    {
        /// <summary>
        /// The title or label of this container (e.g., section header).
        /// </summary>
        protected string _name;

        /// <summary>
        /// The collection of child elements (leaves or other containers).
        /// </summary>
        protected List<IText> _children = new();

        /// <summary>
        /// Reference to the parent container. Null if this is the root.
        /// </summary>
        public Container? Parent { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="name">The name/title of this container.</param>
        /// <param name="parent">The parent container, if any.</param>
        protected Container(string name, Container? parent = null)
        {
            _name = name;
            Parent = parent;
        }

        /// <summary>
        /// Adds a child text element to this container.
        /// </summary>
        /// <param name="child">The text element to add.</param>
        public void AddChild(IText child)
        {
            _children.Add(child);
        }

        /// <summary>
        /// Recursively renders the content of this container and its children.
        /// </summary>
        /// <returns>The combined string output of the hierarchy.</returns>
        public abstract string Render();
    }
}
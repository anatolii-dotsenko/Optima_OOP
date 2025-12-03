public abstract class Container : IText
{
    protected string _name;
    protected List<IText> _children = new();

    protected Container(string name)
    {
        _name = name;
    }

    public void AddChild(IText child)
    {
        _children.Add(child);
    }

    public abstract string Render();
}

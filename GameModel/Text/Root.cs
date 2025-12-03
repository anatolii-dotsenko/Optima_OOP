public class Root : Container
{
    public Root(string name) : base(name)
    {
    }

    public override string Render()
    {
        var sb = new StringBuilder();
        sb.AppendLine(_name);
        _children.ForEach(c => sb.Append(c.Render()));
        return sb.ToString();
    }
}

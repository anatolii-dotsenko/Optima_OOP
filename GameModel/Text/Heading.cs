public class Heading : Container
{
    private readonly int _rank;

    public Heading(string name, int rank) 
        : base(name)
    {
        _rank = rank;
    }

    public override string Render()
    {
        var sb = new StringBuilder();
        sb.AppendLine(new string('\t', _rank) + _name);
        _children.ForEach(c =>
            sb.AppendLine(new string('\t', _rank + 1) + c.Render()));

        return sb.ToString();
    }
}

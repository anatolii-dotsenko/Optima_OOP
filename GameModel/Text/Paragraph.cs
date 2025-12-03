public class Paragraph : Leaf
{
    public Paragraph(string content)
        : base(content)
    {
    }

    public override string Render()
    {
        return (_content ?? string.Empty) + "\n";
    }
}

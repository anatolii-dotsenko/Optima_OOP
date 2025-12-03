public class TextFactory
{
    private Container _current;

    public TextFactory(string title)
    {
        _current = new Root(title);
    }

    public void AddHeading(string name, int rank = 0)
    {
        var heading = new Heading(name, rank);
        _current.AddChild(heading);
        _current = heading;
    }

    public void AddParagraph(string content)
    {
        _current.AddChild(new Paragraph(content));
    }

    public void Up()
    {
        Console.WriteLine("Moving up not supported without Parent links.");
    }

    public override string ToString()
    {
        return _current.Render();
    }
}

namespace GameModel.Text
{
    public enum TextType
    {
        Heading,
        Paragraph
    }

    /// <summary>
    /// Centralizes the creation of text elements (OCP/Factory Pattern).
    /// </summary>
    public class TextFactory
    {
        public void AddElement(DocumentContext context, TextType type, string nameOrContent)
        {
            if (type == TextType.Heading)
            {
                // Default rank 1 for CLI simplicity
                var heading = new Heading(nameOrContent, 1, context.CurrentContainer);
                context.CurrentContainer.AddChild(heading);
                // Auto-enter new containers? Optional. Let's stay in current for now unless CD is used.
                context.CurrentContainer = heading;
            }
            else if (type == TextType.Paragraph)
            {
                context.CurrentContainer.AddChild(new Paragraph(nameOrContent));
            }
        }
    }
}
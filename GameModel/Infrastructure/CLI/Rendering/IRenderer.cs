namespace GameModel.Infrastructure.CLI.Rendering
{
    // generic interface for rendering data of type T
    public interface IRenderer<T>
    {
        void Render(T data);
    }

    // base interface for rendering console output
    public interface IConsoleRenderer
    {
        void WriteMessage(string message);
        void WriteError(string message);
        void DrawSeparator();
    }
}
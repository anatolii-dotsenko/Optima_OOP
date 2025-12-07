namespace GameModel.Infrastructure.CLI.Rendering
{
    // dependency inversion
    // object decides how it wants to be rendered
    public interface IRenderable<T>
    {
        void UseRenderer(IRenderer<T> renderer);
    }
}
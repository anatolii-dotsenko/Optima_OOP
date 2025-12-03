namespace GameModel.Text
{
    public class Root : Container
    {
        public Root(string name) : base(name, null) { }
        
        // Root завжди рендерить все дерево
        public override string Render()
        {
            return base.Render();
        }
    }
}
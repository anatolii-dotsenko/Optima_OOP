namespace GameModel.Text
{
    public class Root : Container
    {
        public Root(string name) : base(name, null) { }
        
        // Root always renders the whole tree
        public override string Render()
        {
            return base.Render();
        }
    }
}
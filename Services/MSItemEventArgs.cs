using MultiSync.Models.Item;

namespace MultiSync.Services
{
    public class MSItemEventArgs : EventArgs
    {
        public MSItem Item { get; set; }
        public String Action {  get; set; }
        public MSItemEventArgs(MSItem item, string action)
        {
            this.Item = item;
            this.Action = action;
        }
    }
}

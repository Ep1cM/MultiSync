using MultiSync.Models.Item;

namespace MultiSync.Services
{
    public class XMLItemEventArgs : EventArgs
    {
        public XMLItem Item { get; set; }
        public String Action { get; set; }
        public XMLItemEventArgs(XMLItem item, string action)
        {
            this.Item = item;
            this.Action = action;
        }
    }
}

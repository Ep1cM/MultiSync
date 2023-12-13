using MultiSync.Models.Item;
using System;

namespace MultiSync.Services
{
    public class BsonItemEventArgs : EventArgs
    {
        public BsonItem Item { get; set; }
        public String Action { get; set; }
        public BsonItemEventArgs(BsonItem item, string action)
        {
            this.Item = item;
            this.Action = action;
        }
    }
}

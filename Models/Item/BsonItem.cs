using MongoDB.Bson;
using System;

namespace MultiSync.Models.Item
{
    public class BsonItem
    {
        public string surrogateId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int quantity { get; set; } = 0;
        public double price { get; set; } = 0.0;
        public bool sync { get; set; }
        public DateTime createTime { get; set; }
        public DateTime changeTime { get; set; }
        public ObjectId _id { get; set; }
    }
}

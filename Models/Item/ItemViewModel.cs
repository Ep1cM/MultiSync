namespace MultiSync.Models.Item
{
    public class ItemViewModel
    {
        public string surrogateId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int quantity { get; set; } = 0;
        public double price { get; set; } = 0.0;
        public bool sync { get; set; }
        public DateTime createTime { get; set; }
        public DateTime changeTime { get; set; }
        public ItemViewModel()
        {
            surrogateId = Guid.NewGuid().ToString();
            createTime = DateTime.Now;
            changeTime = DateTime.Now;
            sync = false;
        }

        public ItemViewModel(string _name, string _description) : this()
        {
            name = _name;
            description = _description;
        }
        public ItemViewModel(string _name, string _description, int _quantity, double _price) : this()
        {
            name = _name;
            description = _description;
            quantity = _quantity;
            price = _price;
        }

    }
}

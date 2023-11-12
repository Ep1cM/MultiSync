using MongoDB.Driver;
using MultiSync.Models.Item;

namespace MultiSync.Repository.MongoDB
{
    public class MongoDBRepo : IMongoDBRepo<BsonItem>
    {
        private readonly IMongoCollection<BsonItem> _items;

        public MongoDBRepo(IMongoDatabase client)
        {
            _items = client.GetCollection<BsonItem>("Items");
        }

        public async Task<BsonItem> GetByIdAsync(string id)
        {
            return await _items.Find(item => item.surrogateId == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<BsonItem>> GetAllAsync()
        {
            return await _items.Find(_ => true).ToListAsync();
        }

        public async Task<BsonItem> CreateAsync(BsonItem item)
        {
            await _items.InsertOneAsync(item);
            return item;
        }

        public async Task UpdateAsync(string id, BsonItem item)
        {
            await _items.ReplaceOneAsync(u => u.surrogateId == id, item);
        }

        public async Task DeleteAsync(string id)
        {
            await _items.DeleteOneAsync(item => item.surrogateId == id);
        }
    }
}

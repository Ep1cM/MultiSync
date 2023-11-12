namespace MultiSync.Repository.MongoDB
{
    public interface IMongoDBRepo<BsonItem>
    {
        Task<BsonItem> GetByIdAsync(string id);
        Task<IEnumerable<BsonItem>> GetAllAsync();
        Task<BsonItem> CreateAsync(BsonItem item);
        Task UpdateAsync(string id, BsonItem item);
        Task DeleteAsync(string id);
    }
}

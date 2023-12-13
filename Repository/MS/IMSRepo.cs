using MultiSync.Services;

namespace MultiSync.Repository.MS
{
    public interface IMSRepo<Item>
    {
        IEnumerable<Item> GetAll();
        Item GetById(String id);
        void Add(Item entity);
        void Update(Item entity);
        void Synced(string itemID);
        void Delete(String entityID);
    }
}

namespace MultiSync.Repository.MS
{
    public interface IMSRepo<Item>
    {
        IEnumerable<Item> GetAll();
        Item GetById(int id);
        void Add(Item entity);
        void Update(Item entity);
        void Delete(Item entity);
    }
}

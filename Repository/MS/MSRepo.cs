using Microsoft.EntityFrameworkCore;
using MultiSync.Data;
using MultiSync.Models.Item;

namespace MultiSync.Repository.MS
{
    public class MSRepo : IMSRepo<MSItem>
    {
        protected readonly ApplicationContext _context;
        private readonly DbSet<MSItem> _entities;

        public MSRepo(ApplicationContext context)
        {
            this._context = context;
            _entities = context.Set<MSItem>();
        }

        public virtual IEnumerable<MSItem> GetAll()
        {
            return _entities.ToList();
        }

        public virtual MSItem GetById(int id)
        {
            return _entities.Find(id);
        }

        public virtual void Add(MSItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            SaveChanges();
        }
        public virtual void AddRange(List<MSItem> entity)
        {
            if (entity.Count == 0)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.AddRange(entity);
            SaveChanges();
        }

        public virtual void Update(MSItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
            SaveChanges();
        }

        public virtual void Delete(MSItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
            SaveChanges();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
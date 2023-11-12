using Microsoft.EntityFrameworkCore;
using MultiSync.Models.Item;

namespace MultiSync.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<MSItem> Items { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
    }
}
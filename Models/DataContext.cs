using Microsoft.EntityFrameworkCore;

namespace HeroBattle.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "HeroDb");
        }
        public virtual DbSet<Hero> Heroes { get; set; }
        public virtual DbSet<Arena> Arenas { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace WEB_API.Models
{
    public class GamesContext : DbContext
    {
        public DbSet<Games> Games { get; set; }
        public GamesContext(DbContextOptions<GamesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using SpelarDuInAPI.Models;

namespace SpelarDuInAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}

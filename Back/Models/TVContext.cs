using Microsoft.EntityFrameworkCore;

namespace Back.Models
{
    public class TVContext : DbContext
    {
        public DbSet<Network> Networks { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Slot> Slots { get; set; }

        public TVContext(DbContextOptions options) : base(options)
        {

        }
    }
}
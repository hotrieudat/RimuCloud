using Microsoft.EntityFrameworkCore;
using RimuCloud.Domain.Entities;

namespace RimuCloud.Persistence.Postgres
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

        public DbSet<Entry> Entries { get; set; }

    }
}

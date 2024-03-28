using Microsoft.EntityFrameworkCore;
using stackExchange.Models;

namespace stackExchange.Database
{
    public class StackOverflowDbContext : DbContext
    {
        public StackOverflowDbContext(DbContextOptions<StackOverflowDbContext> options)
     : base(options) { }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}

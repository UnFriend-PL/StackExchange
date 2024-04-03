using Microsoft.EntityFrameworkCore;
using stackExchange.Models.Tag;
using System.Linq.Expressions;

namespace stackExchange.Database
{
    public class StackOverflowDbContext : DbContext
    {
        public StackOverflowDbContext(DbContextOptions<StackOverflowDbContext> options)
     : base(options) { }

        public StackOverflowDbContext()
        {

        }
        public StackOverflowDbContext(DbSet<Tag> dbSet)
        {
            Tags = dbSet;
        }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public IQueryable<Tag> GetQueryable()
        {
            return Tags;
        }
    }
}





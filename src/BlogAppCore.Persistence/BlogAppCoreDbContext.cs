using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Persistence
{
    public class BlogAppCoreDbContext : DbContext, IBlogAppCoreDbContext
    {
        public BlogAppCoreDbContext(DbContextOptions<BlogAppCoreDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogAppCoreDbContext).Assembly);
        }
    }
}
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogAppCore.Persistence
{
    public class BlogAppCoreDbContext : ApiAuthorizationDbContext<ApplicationUser>, IBlogAppCoreDbContext
    {
        public BlogAppCoreDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }

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

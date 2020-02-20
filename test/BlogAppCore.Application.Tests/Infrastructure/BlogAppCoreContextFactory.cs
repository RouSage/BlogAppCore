using System;
using System.Linq;
using BlogAppCore.Domain.Entities;
using BlogAppCore.Persistence;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogAppCore.Application.Tests.Infrastructure
{
    public class BlogAppCoreContextFactory
    {
        private static readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public static BlogAppCoreDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogAppCoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new BlogAppCoreDbContext(optionsBuilder.Options, _operationalStoreOptions);
            context.Database.EnsureCreated();

            // Seed database
            var tags = new[]
            {
                new Tag("Test Tag 1"),
                new Tag("Test Tag 2"),
                new Tag("Test Tag 3")
            };
            context.Tags.AddRange(tags);
            context.SaveChanges();

            var categories = new[]
            {
                new Category("Test Category 1"),
                new Category("Test Category 2"),
                new Category("Test Category 3")
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            var posts = new[]
            {
                new Post("Test Post 1", "Description 1", "Content 1",
                    categories[0].Id, tags.Select(t => t.Id).Take(2), true),
                new Post("Test Post 2", "Description 2", "Content 2",
                    categories[0].Id, tags.Select(t => t.Id).TakeLast(2), true),
                new Post("Test Post 3", "", "",
                    categories[2].Id, null, false)
            };
            context.Posts.AddRange(posts);
            context.SaveChanges();

            return context;
        }

        public static void Destroy(BlogAppCoreDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using BlogAppCore.Domain.Entities;
using BlogAppCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Tests.Infrastructure
{
    public class BlogAppCoreContextFactory
    {
        public static BlogAppCoreDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogAppCoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new BlogAppCoreDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            // context.Tags.AddRange(new List<Tag>
            // {
            //     new Tag("Test Tag 1"),
            //     new Tag("Test Tag 2"),
            //     new Tag("Test Tag 3")
            // });
            // context.Categories.AddRange(new List<Category>
            // {
            //     new Category("Test Category 1"),
            //     new Category("Test Category 2"),
            //     new Category("Test Category 3")
            // });
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
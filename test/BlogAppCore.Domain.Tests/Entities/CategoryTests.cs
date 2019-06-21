using BlogAppCore.Domain.Entities;
using Xunit;

namespace BlogAppCore.Domain.Tests.Entities
{
    public class CategoryTests
    {
        [Theory]
        [InlineData("ASP.NET Core", "aspnet-core")]
        [InlineData("Development    Environment", "development-environment")]
        [InlineData("", "")]
        public void ShouldCreateCorrectCategoryEntity(string name, string expectedSlug)
        {
            var entity = new Category(name);

            Assert.Equal(name, entity.Name);
            Assert.Equal(expectedSlug, entity.Slug);
            Assert.Empty(entity.Posts);
            Assert.NotNull(entity.Posts);
        }

        [Theory]
        [InlineData("Category 1", "New Category 1", true, "new-category-1")]
        [InlineData("Category-2", "Updated Category-2", false, "category-2")]
        [InlineData("", "New Category", true, "new-category")]
        [InlineData("", "New Category", false, "")]
        public void ShouldUpdateTagCorrectly(string oldName, string newName,
            bool updateSlug, string expectedSlug)
        {
            var entity = new Tag(oldName);
            entity.Update(newName, updateSlug);

            Assert.Equal(newName, entity.Name);
            Assert.Equal(expectedSlug, entity.Slug);
        }
    }
}
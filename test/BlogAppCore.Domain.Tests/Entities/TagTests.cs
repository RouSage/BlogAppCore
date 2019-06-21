using BlogAppCore.Domain.Entities;
using Xunit;

namespace BlogAppCore.Domain.Tests.Entities
{
    public class TagTests
    {
        [Theory]
        [InlineData("ASP.NET Core", "aspnet-core")]
        [InlineData("Visual    Studio", "visual-studio")]
        [InlineData("", "")]
        public void ShouldCreateCorrectTagEntity(string name, string expectedSlug)
        {
            var entity = new Tag(name);

            Assert.Equal(name, entity.Name);
            Assert.Equal(expectedSlug, entity.Slug);
            Assert.Empty(entity.PostTags);
            Assert.NotNull(entity.PostTags);
        }

        [Theory]
        [InlineData("Tag 1", "New Tag 1", true, "new-tag-1")]
        [InlineData("Tag-2", "Updated Tag-2", false, "tag-2")]
        [InlineData("", "New Tag", true, "new-tag")]
        [InlineData("", "New Tag", false, "")]
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
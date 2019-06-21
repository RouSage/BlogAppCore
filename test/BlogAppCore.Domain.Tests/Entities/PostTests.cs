using System.Collections.Generic;
using BlogAppCore.Domain.Entities;
using Xunit;

namespace BlogAppCore.Domain.Tests.Entities
{
    public class PostTests
    {
        [Theory]
        [InlineData("Post Title", "Post Description", "Post content", 1, true, true, "post-title")]
        [InlineData("Post    Title", "", "", 0, false, false, "post-title")]
        public void ShouldCreateCorrectPostEntity(string title, string description, string content,
            int categoryId, bool createTags, bool published, string expectedSlug)
        {
            Post entity;
            if (createTags)
            {
                var tags = new List<int> { 1, 2, 5, 4 };
                entity = new Post(title, description, content, categoryId, tags, published);
            }
            else
            {
                entity = new Post(title, description, content, categoryId, null, published);
            }

            Assert.Equal(title, entity.Title);
            Assert.Equal(expectedSlug, entity.Slug);
            Assert.Equal(description, entity.Description);
            Assert.Equal(content, entity.Content);
            Assert.Equal(categoryId, entity.CategoryId);
            Assert.Equal(published, entity.Published);
            Assert.NotNull(entity.PostTags);

            if (createTags)
                Assert.NotEmpty(entity.PostTags);
            else
                Assert.Empty(entity.PostTags);
        }
    }
}
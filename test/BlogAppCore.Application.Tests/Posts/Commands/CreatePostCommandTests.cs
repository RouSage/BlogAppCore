using System;
using System.Linq;
using System.Threading;
using BlogAppCore.Application.Posts.Commands.Create;
using BlogAppCore.Application.Tests.Infrastructure;
using MediatR;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Commands
{
    public class CreatePostCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldCreateCorrectTagEntity()
        {
            // Arange
            var sut = new CreatePostCommandHandler(_context);

            // Act
            var result = sut.Handle(new CreatePostCommand
            {
                Title = "Test Post 1",
                    Description = "Test Post Description",
                    Content = "Test Post Content",
                    CategoryId = 1,
                    Tags = null,
                    Published = true
            }, CancellationToken.None);
            var entity = _context.Posts.FirstOrDefault(x => x.Title.Equals("Test Post 1"));

            // Assert
            Assert.IsType<Unit>(result.Result);
            Assert.True(entity.Id > 0);
            Assert.Equal("test-post-1", entity.Slug);
            Assert.Equal("Test Post Description", entity.Description);
            Assert.Equal("Test Post Content", entity.Content);
            Assert.Equal(1, entity.CategoryId);
            Assert.Empty(entity.PostTags);
            Assert.True(entity.Created < DateTime.UtcNow);
            Assert.True(entity.Published);
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Posts.Commands.Create;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Application.Tests.Infrastructure;
using MediatR;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Commands
{
    public class CreatePostCommandTests : CommandTestBase
    {
        [Fact]
        public async Task GivenValidRequest_ShouldCreateCorrectTagEntity()
        {
            // Arrange
            var sut = new CreatePostCommandHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new CreatePostCommand
            {
                Title = "New Post 1",
                Description = "Test Post Description",
                Content = "Test Post Content",
                CategoryId = 1,
                Tags = null,
                Published = true
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<PostDetailDto>(result);
            Assert.Equal("new-post-1", result.Slug);
            Assert.Equal("Test Post Description", result.Description);
            Assert.Equal("Test Post Content", result.Content);
            Assert.Empty(result.Tags);
            Assert.True(result.Created < DateTime.UtcNow);
        }
    }
}
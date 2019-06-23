using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Posts.Commands.Update;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Domain.Entities;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Commands
{
    public class UpdatePostCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldUpdatePostCorrectly()
        {
            // Arange
            var sut = new UpdatePostCommandHandler(_context);
            var entity = new Post("Test Post 1", "", "", 1, null, false);

            _context.Posts.Add(entity);
            _context.SaveChanges();

            // Act
            var result = sut.Handle(
                new UpdatePostCommand
                {
                    Id = entity.Id,
                        Title = "Test Post 1",
                        CategoryId = 1,
                        Content = "Content",
                        Description = "Description",
                        Published = true,
                        Tags = null,
                        UpdateSlug = true
                },
                CancellationToken.None);

            Assert.True(entity.Id > 0);
            Assert.Equal("Test Post 1", entity.Title);
            Assert.Equal("test-post-1", entity.Slug);
            Assert.Equal("Description", entity.Description);
            Assert.Equal("Content", entity.Content);
            Assert.Equal(1, entity.CategoryId);
            Assert.Empty(entity.PostTags);
            Assert.True(entity.Published);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arange
            var sut = new UpdatePostCommandHandler(_context);
            var postId = 10;

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(
                new UpdatePostCommand
                {
                    Id = postId,
                        Title = "New Title",
                        UpdateSlug = true
                },
                CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
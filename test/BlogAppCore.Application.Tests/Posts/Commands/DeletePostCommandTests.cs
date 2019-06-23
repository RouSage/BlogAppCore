using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Posts.Commands.Delete;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Domain.Entities;
using MediatR;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Commands
{
    public class DeletePostCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldSuccessfullyDeletePost()
        {
            // Arange
            var sut = new DeletePostCommandHandler(_context);
            var entity = new Post("Test Post 1", "Description", "Content", 1, null, true);

            _context.Posts.Add(entity);
            _context.SaveChanges();

            // Act
            var result = sut.Handle(new DeletePostCommand { Id = entity.Id }, CancellationToken.None);
            var post = _context.Posts.FirstOrDefault(i => i.Id == entity.Id);

            // Assert
            Assert.IsType<Unit>(result.Result);
            Assert.Null(post);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arange
            var sut = new DeletePostCommandHandler(_context);
            var postId = 10;

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(
                new DeletePostCommand { Id = postId }, CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
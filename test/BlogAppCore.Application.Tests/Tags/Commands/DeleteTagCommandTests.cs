using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Tags.Commands.Delete;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Domain.Entities;
using MediatR;
using Xunit;

namespace BlogAppCore.Application.Tests.Tags.Commands
{
    [Trait("Category", "Commands")]
    public class DeleteTagCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldSuccessfullyDeleteTag()
        {
            // Arrange
            var sut = new DeleteTagCommandHandler(_context);
            var entity = new Tag("Test Tag 1");

            _context.Tags.Add(entity);
            _context.SaveChanges();

            // Act
            var result = sut.Handle(new DeleteTagCommand { Id = entity.Id }, CancellationToken.None);
            var tag = _context.Tags.FirstOrDefault(i => i.Id == entity.Id);

            // Assert
            Assert.IsType<Unit>(result.Result);
            Assert.Null(tag);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var sut = new DeleteTagCommandHandler(_context);
            var tagId = 10;

            // Act (Assert)
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(new DeleteTagCommand
            {
                Id = tagId
            },
            CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
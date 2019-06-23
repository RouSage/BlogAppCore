using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Tags.Commands.Update;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Domain.Entities;
using Xunit;

namespace BlogAppCore.Application.Tests.Tags.Commands
{
    public class UpdateTagCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldUpdateTagCorrectly()
        {
            // Arrange
            var sut = new UpdateTagCommandHandler(_context);
            var entity = new Tag("Test Tag 1");

            _context.Tags.Add(entity);
            _context.SaveChanges();

            // Act
            var result = sut.Handle(
                new UpdateTagCommand
                {
                    Id = entity.Id, Name = "New Tag 1 Name", UpdateSlug = true
                },
                CancellationToken.None);

            Assert.True(entity.Id > 0);
            Assert.Equal("New Tag 1 Name", entity.Name);
            Assert.Equal("new-tag-1-name", entity.Slug);
            Assert.Empty(entity.PostTags);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            var sut = new UpdateTagCommandHandler(_context);
            var tagId = 10;

            // Act (Assert)
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(
                new UpdateTagCommand
                {
                    Id = tagId, Name = "", UpdateSlug = false
                },
                CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
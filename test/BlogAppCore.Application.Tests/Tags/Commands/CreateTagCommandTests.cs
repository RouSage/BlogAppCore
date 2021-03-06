using System;
using System.Linq;
using System.Threading;
using BlogAppCore.Application.Tags.Commands.Create;
using BlogAppCore.Application.Tests.Infrastructure;
using MediatR;
using Xunit;

namespace BlogAppCore.Application.Tests.Tags.Commands
{
    [Trait("Category", "Commands")]
    public class CreateTagCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldCreateCorrectTagEntity()
        {
            // Arrange
            var sut = new CreateTagCommandHandler(_context);
            var newTagName = "New Tag 1";

            // Act
            var result = sut.Handle(new CreateTagCommand { Name = newTagName }, CancellationToken.None);
            var entity = _context.Tags.FirstOrDefault(x => x.Name.Equals(newTagName));

            // Assert
            Assert.IsType<Unit>(result.Result);
            Assert.True(entity.Id > 0);
            Assert.Equal("new-tag-1", entity.Slug);
            Assert.Empty(entity.PostTags);
            Assert.True(entity.Created < DateTime.UtcNow);
        }
    }
}
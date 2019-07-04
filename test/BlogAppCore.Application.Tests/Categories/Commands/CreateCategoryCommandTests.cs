using System;
using System.Linq;
using System.Threading;
using BlogAppCore.Application.Categories.Commands.Create;
using BlogAppCore.Application.Tests.Infrastructure;
using MediatR;
using Xunit;

namespace BlogAppCore.Application.Tests.Categories.Commands
{
    [Trait("Category", "Commands")]
    public class CreateCategoryCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldCreateCorrectCategoryEntity()
        {
            // Arrange
            var sut = new CreateCategoryCommandHandler(_context);
            var newCategoryName = "New Category 1";

            // Act
            var result = sut.Handle(new CreateCategoryCommand
            {
                Name = newCategoryName
            },
            CancellationToken.None);
            var entity = _context.Categories.FirstOrDefault(x => x.Name.Equals(newCategoryName));

            // Assert
            Assert.IsType<Unit>(result.Result);
            Assert.True(entity.Id > 0);
            Assert.Equal("new-category-1", entity.Slug);
            Assert.Empty(entity.Posts);
            Assert.True(entity.Created < DateTime.UtcNow);
        }
    }
}
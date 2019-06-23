using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Categories.Commands.Update;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Domain.Entities;
using Xunit;

namespace BlogAppCore.Application.Tests.Categories.Commands
{
    public class UpdateCategoryCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldUpdateCategoryCorrectly()
        {
            // Arange
            var sut = new UpdateCategoryCommandHandler(_context);
            var entity = new Category("Test Category 1");

            _context.Categories.Add(entity);
            _context.SaveChanges();

            // Act
            var result = sut.Handle(
                new UpdateCategoryCommand
                {
                    Id = entity.Id, Name = "New Category 1 Name", UpdateSlug = true
                },
                CancellationToken.None);

            Assert.True(entity.Id > 0);
            Assert.Equal("New Category 1 Name", entity.Name);
            Assert.Equal("new-category-1-name", entity.Slug);
            Assert.Empty(entity.Posts);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arange
            var sut = new UpdateCategoryCommandHandler(_context);
            var categoryId = 10;

            // Act (Assert)
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(
                new UpdateCategoryCommand
                {
                    Id = categoryId, Name = "", UpdateSlug = false
                },
                CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
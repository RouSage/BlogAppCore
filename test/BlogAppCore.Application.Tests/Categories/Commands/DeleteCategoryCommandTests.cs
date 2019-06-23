using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Categories.Commands.Delete;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Domain.Entities;
using MediatR;
using Xunit;

namespace BlogAppCore.Application.Tests.Categories.Commands
{
    public class DeleteCategoryCommandTests : CommandTestBase
    {
        [Fact]
        public void GivenValidRequest_ShouldSuccessfullyDeleteCategory()
        {
            // Arange
            var sut = new DeleteCategoryCommandHandler(_context);
            var entity = new Category("Test Category 1");

            _context.Categories.Add(entity);
            _context.SaveChanges();

            // Act
            var result = sut.Handle(new DeleteCategoryCommand { Id = entity.Id }, CancellationToken.None);
            var category = _context.Tags.FirstOrDefault(i => i.Id == entity.Id);

            // Assert
            Assert.IsType<Unit>(result.Result);
            Assert.Null(category);
        }

        [Fact]
        public async Task GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arange
            var sut = new DeleteCategoryCommandHandler(_context);
            var categoryId = 10;

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(
                new DeleteCategoryCommand { Id = categoryId }, CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }

        [Fact]
        public async Task GivenValidCategoryWithPost_ShouldThrowDeleteFailureException()
        {
            // Arange
            var sut = new DeleteCategoryCommandHandler(_context);
            var category = new Category("Test Category 1");

            _context.Categories.Add(category);
            _context.SaveChanges();

            var post = new Post("Test Post 1", "Description", "Content", category.Id, null, true);

            _context.Posts.Add(post);
            _context.SaveChanges();

            // Act
            var ex = await Assert.ThrowsAsync<DeleteFailureException>(() => sut.Handle(
                new DeleteCategoryCommand { Id = category.Id }, CancellationToken.None));

            // Assert
            Assert.IsType<DeleteFailureException>(ex);
            Assert.NotNull(category);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Categories.Queries.GetCategoryDetail;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Categories.Queries
{
    [Collection("QueryCollection")]
    [Trait("Category", "Queries")]
    public class GetCategoryDetailQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCategoryDetailTest_ShouldReturnCorrectEntity()
        {
            // Arrange
            var sut = new GetCategoryDetailQueryHandler(_context, _mapper);
            var categoryId = 1;

            // Act
            var result = await sut.Handle(new GetCategoryDetailQuery
            {
                Id = categoryId
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<CategoryDetailDto>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("Test Category 1", result.Name);
            Assert.Equal("test-category-1", result.Slug);
            Assert.Equal(2, result.TotalPosts);
        }

        [Fact]
        public async Task GetCategoryDetailTest_ShouldThrowNotFoundException()
        {
            // Arrange
            var sut = new GetCategoryDetailQueryHandler(_context, _mapper);
            var categoryId = 100;

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(new GetCategoryDetailQuery
            {
                Id = categoryId
            },
            CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
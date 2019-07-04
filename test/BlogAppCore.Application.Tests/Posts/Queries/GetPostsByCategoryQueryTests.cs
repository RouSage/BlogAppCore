using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Application.Posts.Queries.GetPostsByCategory;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Queries
{
    [Collection("QueryCollection")]
    [Trait("Category", "Queries")]
    public class GetPostsByCategoryQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPostsByCategoryQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPostsByCategoryTest_ShouldReturnCorrectCollection()
        {
            // Arrange
            var sut = new GetPostsByCategoryQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPostsByCategoryQuery
            {
                CategorySlug = "test-category-1"
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<List<PostPreviewDto>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetPostsByCategoryTest_ShouldReturnEmptyCollection()
        {
            // Arrange
            var sut = new GetPostsByCategoryQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPostsByCategoryQuery
            {
                CategorySlug = "random-category"
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<List<PostPreviewDto>>(result);
            Assert.Empty(result);
        }
    }
}
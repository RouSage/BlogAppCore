using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Application.Posts.Queries.GetPostDetail;
using BlogAppCore.Application.Tags.Models;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Queries
{
    [Collection("QueryCollection")]
    [Trait("Category", "Queries")]
    public class GetPostDetailQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPostDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPostDetailTest_ShouldReturnCorrectEntity()
        {
            // Arrange
            var sut = new GetPostDetailQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPostDetailQuery
            {
                Slug = "test-post-1"
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<PostDetailDto>(result);
            Assert.IsType<CategoryPreviewDto>(result.Category);
            Assert.IsType<List<TagPreviewDto>>(result.Tags);
            Assert.Equal("Test Post 1", result.Title);
            Assert.Equal("Description 1", result.Description);
            Assert.Equal("Content 1", result.Content);
            Assert.Equal("Test Category 1", result.Category.Name);
            Assert.Equal("test-category-1", result.Category.Slug);
            Assert.NotNull(result.Category);
            Assert.NotEmpty(result.Tags);
        }

        [Fact]
        public async Task GetPostDetailQueryTest_ShouldThrowNotFoundException()
        {
            // Arrange
            var sut = new GetPostDetailQueryHandler(_context, _mapper);

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(new GetPostDetailQuery
            {
                Slug = "not-existing-slug"
            },
            CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
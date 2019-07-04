using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Application.Posts.Queries.GetPostsByTag;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Queries
{
    [Collection("QueryCollection")]
    [Trait("Category", "Queries")]
    public class GetPostsByTagQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPostsByTagQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPostsByTagTest_ShouldReturnCorrectCollection()
        {
            // Arrange
            var sut = new GetPostsByTagQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPostsByTagQuery
            {
                TagSlug = "test-tag-1"
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<List<PostPreviewDto>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetPostsByTagTest_ShouldReturnEmpyCollection()
        {
            // Arrange
            var sut = new GetPostsByTagQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPostsByTagQuery
            {
                TagSlug = "random-tag"
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<List<PostPreviewDto>>(result);
            Assert.Empty(result);
        }
    }
}
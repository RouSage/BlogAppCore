using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Tags.Models;
using BlogAppCore.Application.Tags.Queries.GetTagDetail;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Tags.Queries
{
    [Collection("QueryCollection")]
    [Trait("Category", "Queries")]
    public class GetTagDetailQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetTagDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTagDetailTest_ShouldReturnCorrectEntity()
        {
            // Arrange
            var sut = new GetTagDetailQueryHandler(_context, _mapper);
            var tagId = 1;

            // Act
            var result = await sut.Handle(new GetTagDetailQuery
            {
                Id = tagId
            },
            CancellationToken.None);

            // Assert
            Assert.IsType<TagDetailDto>(result);
            Assert.Equal(tagId, result.Id);
            Assert.Equal("Test Tag 1", result.Name);
            Assert.Equal("test-tag-1", result.Slug);
            Assert.Equal(1, result.TotalPosts);
        }

        [Fact]
        public async Task GetTagDetailQueryTest_ShouldThrowNotFoundException()
        {
            // Arrange
            var sut = new GetTagDetailQueryHandler(_context, _mapper);
            var tagId = 10;

            // Act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(new GetTagDetailQuery
            {
                Id = tagId
            },
            CancellationToken.None));

            // Assert
            Assert.IsType<NotFoundException>(ex);
        }
    }
}
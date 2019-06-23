using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Application.Posts.Queries.GetPostsPreview;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Posts.Queries
{
    [Collection("QueryCollection")]
    public class GetPostsPreviewQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPostsPreviewQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetPostsPreviewTest()
        {
            // Arrange
            var sut = new GetPostsPreviewQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetPostsPreviewQuery(), CancellationToken.None);

            // Assert
            Assert.IsType<List<PostPreviewDto>>(result);
            Assert.NotEmpty(result);
        }
    }
}
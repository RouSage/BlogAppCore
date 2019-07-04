using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Tags.Models;
using BlogAppCore.Application.Tags.Queries.GetTagList;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Tags.Queries
{
    [Collection("QueryCollection")]
    [Trait("Category", "Queries")]
    public class GetTagListQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetTagListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetTagListTest()
        {
            // Arrange
            var sut = new GetTagListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetTagListQuery(), CancellationToken.None);

            // Assert
            Assert.IsType<List<TagPreviewDto>>(result);
            Assert.Equal(3, result.Count);
        }
    }
}
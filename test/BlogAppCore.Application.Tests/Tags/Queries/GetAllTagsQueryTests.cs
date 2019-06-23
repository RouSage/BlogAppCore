using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Tags.Commands.Queries.GetAllTags;
using BlogAppCore.Application.Tags.Models;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Tags.Queries
{
    [Collection("QueryCollection")]
    public class GetAllTagsQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTagsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllTagsTest()
        {
            // Arrange
            var sut = new GetAllTagsQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetAllTagsQuery(), CancellationToken.None);

            // Assert
            Assert.IsType<List<TagDetailDto>>(result);
            Assert.Equal(3, result.Count);
        }
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Categories.Queries.GetAllCategories;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Categories.Queries
{
    [Collection("QueryCollection")]
    public class GetAllCategoriesQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllCategoriesTest()
        {
            // Arrange
            var sut = new GetAllCategoriesQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            Assert.IsType<List<CategoryDetailDto>>(result);
            Assert.Equal(3, result.Count);
        }
    }
}
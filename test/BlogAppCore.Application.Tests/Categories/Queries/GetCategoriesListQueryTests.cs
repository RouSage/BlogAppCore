using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Categories.Queries.GetCategoriesList;
using BlogAppCore.Application.Tests.Infrastructure;
using BlogAppCore.Persistence;
using Xunit;

namespace BlogAppCore.Application.Tests.Categories.Queries
{
    [Collection("QueryCollection")]
    [Trait("Category", "Queries")]
    public class GetCategoriesListQueryTests
    {
        private readonly BlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCategoriesListTest()
        {
            // Arrange
            var sut = new GetCategoriesListQueryHandler(_context, _mapper);

            // Act
            var result = await sut.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            // Assert
            Assert.IsType<List<CategoryListDto>>(result);
            Assert.Equal(3, result.Count);
        }
    }
}
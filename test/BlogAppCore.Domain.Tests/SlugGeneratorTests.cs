using Xunit;

namespace BlogAppCore.Domain.Tests
{
    public class SlugGeneratorTests
    {
        [Theory]
        [InlineData("ASP.NET Core Introduction", 25, "aspnet-core-introduction")]
        [InlineData("Should cut string", 10, "should-cut")]
        [InlineData("TEST Title 1", 30, "test-title-1")]
        [InlineData("Should     Remove         SPACES", 20, "should-remove-spaces")]
        public void ShouldCreateCorrectSlug(string value, int length, string expected)
        {
            var slug = value.GenerateSlug(length);

            Assert.Equal(expected, slug);
            Assert.True(slug.Length <= length);
        }
    }
}
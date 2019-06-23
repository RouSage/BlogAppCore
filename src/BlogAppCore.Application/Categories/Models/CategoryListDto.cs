namespace BlogAppCore.Application.Categories.Models
{
    public class CategoryListDto
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public int TotalPosts { get; set; }
    }
}
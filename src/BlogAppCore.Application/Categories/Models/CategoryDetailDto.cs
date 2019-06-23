using System;

namespace BlogAppCore.Application.Categories.Models
{
    public class CategoryDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public DateTime Created { get; set; }

        public int TotalPosts { get; set; }
    }
}
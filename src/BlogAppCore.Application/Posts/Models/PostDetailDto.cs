using System;
using System.Collections.Generic;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Tags.Models;

namespace BlogAppCore.Application.Posts.Models
{
    public class PostDetailDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public CategoryPreviewDto Category { get; set; }

        public List<TagPreviewDto> Tags { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace BlogAppCore.Domain.Entities
{
    public class Post : BaseEntity
    {
        public Post(
            string title,
            string description,
            string content,
            int categoryId,
            IEnumerable<int> tags = null,
            bool published = false)
        {
            Title = title;
            Slug = title.GenerateSlug(30);
            Description = description;
            Content = content;
            Published = published;
            CategoryId = categoryId;
            _postTags = new List<PostTag>();

            if (tags != null)
            {
                foreach (var tagId in tags)
                {
                    AddTag(tagId);
                }
            }
        }

        private Post() { }

        public string Title { get; private set; }

        public string Slug { get; private set; }

        public string Description { get; private set; }

        public string Content { get; private set; }

        public bool Published { get; private set; }

        public int CategoryId { get; private set; }

        public Category Category { get; private set; }

        private List<PostTag> _postTags;

        public IEnumerable<PostTag> PostTags => _postTags?.ToList();

        private void AddTag(int tagId)
        {
            _postTags.Add(new PostTag(Id, tagId));
        }
    }
}
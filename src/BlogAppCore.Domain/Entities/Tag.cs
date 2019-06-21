using System.Collections.Generic;
using System.Linq;

namespace BlogAppCore.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public const int MAX_LENGTH = 20;

        public Tag(string name)
        {
            Name = name.Trim();
            Slug = name.GenerateSlug(MAX_LENGTH);
            _postTags = new List<PostTag>();
        }

        private Tag() { }

        public string Name { get; private set; }

        public string Slug { get; private set; }

        private readonly List<PostTag> _postTags;

        public IEnumerable<PostTag> PostTags => _postTags?.ToList();

        public void Update(string name, bool updateSlug = false)
        {
            Name = name.Trim();
            Slug = updateSlug ? name.GenerateSlug(MAX_LENGTH) : Slug;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace BlogAppCore.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public Tag(string name)
        {
            Name = name;
            Slug = name.GenerateSlug(20);
            _postTags = new List<PostTag>();
        }

        private Tag() { }

        public string Name { get; private set; }

        public string Slug { get; private set; }

        private readonly List<PostTag> _postTags;

        public IEnumerable<PostTag> PostTags => _postTags?.ToList();
    }
}
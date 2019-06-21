using System.Collections.Generic;
using System.Linq;

namespace BlogAppCore.Domain.Entities
{
    public class Category : BaseEntity
    {
        public const int MAX_LENGTH = 30;

        public Category(string name)
        {
            Name = name.Trim();
            Slug = name.GenerateSlug(MAX_LENGTH);
            _posts = new List<Post>();
        }

        private Category() { }

        public string Name { get; private set; }

        public string Slug { get; private set; }

        private List<Post> _posts;

        public IEnumerable<Post> Posts => _posts?.ToList();

        public void Update(string name, bool updateSlug = false)
        {
            Name = name.Trim();
            Slug = updateSlug ? name.GenerateSlug(MAX_LENGTH) : Slug;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace BlogAppCore.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category(string name)
        {
            Name = name;
            Slug = name.GenerateSlug(30);
            _posts = new List<Post>();
        }

        private Category() { }

        public string Name { get; private set; }

        public string Slug { get; private set; }

        private List<Post> _posts;

        public IEnumerable<Post> Posts => _posts?.ToList();
    }
}
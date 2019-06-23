using System.Collections.Generic;
using MediatR;

namespace BlogAppCore.Application.Posts.Commands.Update
{
    public class UpdatePostCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> Tags { get; set; }

        public bool Published { get; set; }

        public bool UpdateSlug { get; set; }
    }
}
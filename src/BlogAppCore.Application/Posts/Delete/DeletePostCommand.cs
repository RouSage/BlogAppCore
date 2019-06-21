using MediatR;

namespace BlogAppCore.Application.Posts.Delete
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }
    }
}
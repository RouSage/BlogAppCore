using MediatR;

namespace BlogAppCore.Application.Posts.Commands.Delete
{
    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }
    }
}
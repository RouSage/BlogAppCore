using MediatR;

namespace BlogAppCore.Application.Tags.Commands.Delete
{
    public class DeleteTagCommand : IRequest
    {
        public int Id { get; set; }
    }
}
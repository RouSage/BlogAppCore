using MediatR;

namespace BlogAppCore.Application.Tags.Commands.Create
{
    public class CreateTagCommand : IRequest
    {
        public string Name { get; set; }
    }
}
using MediatR;

namespace BlogAppCore.Application.Tags.Commands.Update
{
    public class UpdateTagCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool UpdateSlug { get; set; }
    }
}
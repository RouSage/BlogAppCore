using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;

namespace BlogAppCore.Application.Tags.Commands.Create
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Unit>
    {
        private readonly IBlogAppCoreDbContext _context;

        public CreateTagCommandHandler(IBlogAppCoreDbContext context, IMediator mediator)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tag(request.Name);

            _context.Tags.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
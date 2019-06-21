using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Tags.Commands.Delete
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
    {
        private readonly IBlogAppCoreDbContext _context;

        public DeleteTagCommandHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Tag), nameof(Tag.Id), request.Id);
            }

            _context.Tags.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
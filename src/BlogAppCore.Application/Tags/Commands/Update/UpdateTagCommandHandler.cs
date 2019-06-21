using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Tags.Commands.Update
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Unit>
    {
        private readonly IBlogAppCoreDbContext _context;

        public UpdateTagCommandHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Tag), nameof(Tag.Id), request.Id);
            }

            entity.Update(request.Name, request.UpdateSlug);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
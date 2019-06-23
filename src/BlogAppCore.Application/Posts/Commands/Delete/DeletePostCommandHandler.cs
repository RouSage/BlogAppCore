using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Posts.Commands.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
    {
        private readonly IBlogAppCoreDbContext _context;

        public DeletePostCommandHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Posts
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Post), nameof(Post.Id), request.Id);
            }

            _context.Posts.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
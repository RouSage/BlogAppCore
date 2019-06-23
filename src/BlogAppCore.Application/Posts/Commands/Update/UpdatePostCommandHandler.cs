using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Posts.Commands.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
    {
        private readonly IBlogAppCoreDbContext _context;

        public UpdatePostCommandHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Posts
                .Include(x => x.PostTags)
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Post), nameof(Post.Id), request.Id);
            }

            entity.Update(request.Title, request.Description, request.Content, request.CategoryId,
                request.Tags, request.Published, request.UpdateSlug);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
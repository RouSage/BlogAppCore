using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;

namespace BlogAppCore.Application.Posts.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Unit>
    {
        private readonly IBlogAppCoreDbContext _context;

        public CreatePostCommandHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = new Post(request.Title, request.Description, request.Content,
                request.CategoryId, request.Tags, request.Published);

            _context.Posts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
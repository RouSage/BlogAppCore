using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IBlogAppCoreDbContext _context;

        public DeleteCategoryCommandHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), nameof(Category.Id), request.Id);
            }

            var hasPosts = _context.Posts.Any(p => p.CategoryId == entity.Id);
            if (hasPosts)
            {
                throw new DeleteFailureException(nameof(Category), nameof(Category.Name),
                    entity.Name, "There are existing posts associated with this category.");
            }

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Domain.Entities;
using MediatR;

namespace BlogAppCore.Application.Posts.Commands.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDetailDto>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDetailDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var entity = new Post(request.Title, request.Description, request.Content,
                request.CategoryId, request.Tags, request.Published);

            _context.Posts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PostDetailDto>(entity);
        }
    }
}
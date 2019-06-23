using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Posts.Queries.GetPostDetail
{
    public class GetPostDetailQueryHandler : IRequestHandler<GetPostDetailQuery, PostDetailDto>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPostDetailQueryHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDetailDto> Handle(GetPostDetailQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(pt => pt.PostTags).ThenInclude(t => t.Tag)
                .Where(p => p.Slug.Equals(request.Slug) && p.Published == true)
                .ProjectTo<PostDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (post == null)
            {
                throw new NotFoundException(nameof(Post), nameof(Post.Slug), request.Slug);
            }

            return post;
        }
    }
}
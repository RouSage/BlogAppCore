using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Application.Posts.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Posts.Queries.GetPostsPreview
{
    public class GetPostsPreviewQueryHandler : IRequestHandler<GetPostsPreviewQuery, List<PostPreviewDto>>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPostsPreviewQueryHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PostPreviewDto>> Handle(GetPostsPreviewQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Include(c => c.Category)
                .Include(pt => pt.PostTags).ThenInclude(t => t.Tag)
                .Where(p => p.Published == true)
                .ProjectTo<PostPreviewDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Created)
                .ToListAsync(cancellationToken);
        }
    }
}
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

namespace BlogAppCore.Application.Posts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQueryHandler : IRequestHandler<GetPostsByCategoryQuery, List<PostPreviewDto>>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetPostsByCategoryQueryHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PostPreviewDto>> Handle(GetPostsByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(pt => pt.PostTags).ThenInclude(t => t.Tag)
                .Where(p => p.Published == true && p.Category.Slug.Equals(request.CategorySlug))
                .ProjectTo<PostPreviewDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(p => p.Created)
                .ToListAsync(cancellationToken);
        }
    }
}
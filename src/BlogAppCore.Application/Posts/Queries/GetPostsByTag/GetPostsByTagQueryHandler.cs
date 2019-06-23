using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Application.Posts.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Posts.Queries.GetPostsByTag
{
    public class GetPostsByTagQueryHandler : IRequestHandler<GetPostsByTagQuery, List<PostPreviewDto>>
    {
        private readonly IBlogAppCoreDbContext _context;

        public GetPostsByTagQueryHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostPreviewDto>> Handle(GetPostsByTagQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .AsNoTracking()
                .Include(c => c.Category)
                .Include(pt => pt.PostTags).ThenInclude(t => t.Tag)
                .Where(
                    p => p.Published == true &&
                    p.PostTags.Any(t => t.Tag.Slug.Equals(request.TagSlug)))
                .ProjectTo<PostPreviewDto>()
                .OrderByDescending(p => p.Created)
                .ToListAsync(cancellationToken);
        }
    }
}
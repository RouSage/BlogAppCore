using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public GetPostsPreviewQueryHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostPreviewDto>> Handle(GetPostsPreviewQuery request, CancellationToken cancellationToken)
        {
            return await _context.Posts
                .Include(c => c.Category)
                .Include(pt => pt.PostTags).ThenInclude(t => t.Tag)
                .Where(p => p.Published == true)
                .ProjectTo<PostPreviewDto>()
                .OrderByDescending(p => p.Created)
                .ToListAsync(cancellationToken);
        }
    }
}
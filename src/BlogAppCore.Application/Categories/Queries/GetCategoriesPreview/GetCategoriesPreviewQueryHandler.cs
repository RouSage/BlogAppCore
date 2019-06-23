using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Categories.Queries.GetCategoriesPreview
{
    public class GetCategoriesPreviewQueryHandler : IRequestHandler<GetCategoriesPreviewQuery, List<CategoryListDto>>
    {
        private readonly IBlogAppCoreDbContext _context;

        public GetCategoriesPreviewQueryHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryListDto>> Handle(GetCategoriesPreviewQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .ProjectTo<CategoryListDto>()
                .OrderBy(n => n.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDetailDto>>
    {
        private readonly IBlogAppCoreDbContext _context;

        public GetAllCategoriesQueryHandler(IBlogAppCoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDetailDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .ProjectTo<CategoryDetailDto>()
                .OrderByDescending(c => c.Created)
                .ToListAsync(cancellationToken);
        }
    }
}
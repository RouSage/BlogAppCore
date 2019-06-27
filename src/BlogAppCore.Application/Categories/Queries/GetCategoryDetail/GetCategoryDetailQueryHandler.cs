using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Categories.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryDetailDto>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryDetailQueryHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDetailDto> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories
                .AsNoTracking()
                .Where(i => i.Id == request.Id)
                .ProjectTo<CategoryDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), nameof(Category.Id), request.Id);
            }

            return entity;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Application.Tags.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Tags.Queries.GetTagList
{
    public class GetTagListQueryHandler : IRequestHandler<GetTagListQuery, List<TagPreviewDto>>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetTagListQueryHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TagPreviewDto>> Handle(GetTagListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tags
                .AsNoTracking()
                .ProjectTo<TagPreviewDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(t => t.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
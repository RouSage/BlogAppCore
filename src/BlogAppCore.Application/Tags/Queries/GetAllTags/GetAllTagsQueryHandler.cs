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

namespace BlogAppCore.Application.Tags.Commands.Queries.GetAllTags
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<TagDetailDto>>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTagsQueryHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TagDetailDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tags
                .AsNoTracking()
                .ProjectTo<TagDetailDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(c => c.Created)
                .ToListAsync(cancellationToken);
        }
    }
}
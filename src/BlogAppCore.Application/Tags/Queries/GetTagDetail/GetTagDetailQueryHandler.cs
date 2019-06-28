using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogAppCore.Application.Exceptions;
using BlogAppCore.Application.Interfaces;
using BlogAppCore.Application.Tags.Models;
using BlogAppCore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAppCore.Application.Tags.Queries.GetTagDetail
{
    public class GetTagDetailQueryHandler : IRequestHandler<GetTagDetailQuery, TagDetailDto>
    {
        private readonly IBlogAppCoreDbContext _context;
        private readonly IMapper _mapper;

        public GetTagDetailQueryHandler(IBlogAppCoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TagDetailDto> Handle(GetTagDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags
                .AsNoTracking()
                .Where(i => i.Id == request.Id)
                .ProjectTo<TagDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Tag), nameof(Tag.Id), request.Id);
            }

            return entity;
        }
    }
}
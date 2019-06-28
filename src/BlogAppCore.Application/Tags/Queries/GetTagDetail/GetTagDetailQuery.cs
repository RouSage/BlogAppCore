using BlogAppCore.Application.Tags.Models;
using MediatR;

namespace BlogAppCore.Application.Tags.Queries.GetTagDetail
{
    public class GetTagDetailQuery : IRequest<TagDetailDto>
    {
        public int Id { get; set; }
    }
}
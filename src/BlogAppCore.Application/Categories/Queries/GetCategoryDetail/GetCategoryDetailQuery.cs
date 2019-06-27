using BlogAppCore.Application.Categories.Models;
using MediatR;

namespace BlogAppCore.Application.Categories.Queries.GetCategoryDetail
{
    public class GetCategoryDetailQuery : IRequest<CategoryDetailDto>
    {
        public int Id { get; set; }
    }
}
using System.Collections.Generic;
using BlogAppCore.Application.Categories.Models;
using MediatR;

namespace BlogAppCore.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDetailDto>>
    {

    }
}
using System.Collections.Generic;
using BlogAppCore.Application.Categories.Models;
using MediatR;

namespace BlogAppCore.Application.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<List<CategoryListDto>>
    {

    }
}
using System.Collections.Generic;
using BlogAppCore.Application.Categories.Models;
using MediatR;

namespace BlogAppCore.Application.Categories.Queries.GetCategoriesPreview
{
    public class GetCategoriesPreviewQuery : IRequest<List<CategoryListDto>>
    {

    }
}
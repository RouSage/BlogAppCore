using System.Collections.Generic;
using BlogAppCore.Application.Tags.Models;
using MediatR;

namespace BlogAppCore.Application.Tags.Queries.GetTagList
{
    public class GetTagListQuery : IRequest<List<TagPreviewDto>>
    {

    }
}
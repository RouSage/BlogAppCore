using System.Collections.Generic;
using BlogAppCore.Application.Tags.Models;
using MediatR;

namespace BlogAppCore.Application.Tags.Commands.Queries.GetAllTags
{
    public class GetAllTagsQuery : IRequest<List<TagDetailDto>>
    {

    }
}
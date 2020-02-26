using System.Collections.Generic;
using System.Threading.Tasks;
using BlogAppCore.Application.Tags.Commands.Create;
using BlogAppCore.Application.Tags.Commands.Delete;
using BlogAppCore.Application.Tags.Commands.Update;
using BlogAppCore.Application.Tags.Models;
using BlogAppCore.Application.Tags.Queries.GetAllTags;
using BlogAppCore.Application.Tags.Queries.GetTagDetail;
using BlogAppCore.Application.Tags.Queries.GetTagList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppCore.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TagsController : BaseController
    {
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDetailDto>>> GetAll()
        {
            return await Mediator.Send(new GetAllTagsQuery());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagPreviewDto>>> GetList()
        {
            return await Mediator.Send(new GetTagListQuery());
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<TagDetailDto>> Get(int id)
        {
            return await Mediator.Send(new GetTagDetailQuery { Id = id });
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateTagCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTagCommand { Id = id });

            return NoContent();
        }
    }
}

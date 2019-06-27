using System.Threading.Tasks;
using BlogAppCore.Application.Categories.Commands.Create;
using BlogAppCore.Application.Categories.Commands.Update;
using BlogAppCore.Application.Categories.Queries.GetAllCategories;
using BlogAppCore.Application.Categories.Queries.GetCategoryDetail;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppCore.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryDetailQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
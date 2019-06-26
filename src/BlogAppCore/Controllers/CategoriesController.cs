using System.Threading.Tasks;
using BlogAppCore.Application.Categories.Commands.Create;
using BlogAppCore.Application.Categories.Queries.GetAllCategories;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
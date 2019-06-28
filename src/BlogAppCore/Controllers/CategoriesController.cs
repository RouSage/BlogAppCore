using System.Collections.Generic;
using System.Threading.Tasks;
using BlogAppCore.Application.Categories.Commands.Create;
using BlogAppCore.Application.Categories.Commands.Delete;
using BlogAppCore.Application.Categories.Commands.Update;
using BlogAppCore.Application.Categories.Models;
using BlogAppCore.Application.Categories.Queries.GetAllCategories;
using BlogAppCore.Application.Categories.Queries.GetCategoriesList;
using BlogAppCore.Application.Categories.Queries.GetCategoryDetail;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppCore.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDetailDto>>> GetAll()
        {
            return await Mediator.Send(new GetAllCategoriesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetailDto>> Get(int id)
        {
            return await Mediator.Send(new GetCategoryDetailQuery { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryListDto>>> GetList()
        {
            return await Mediator.Send(new GetCategoriesListQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
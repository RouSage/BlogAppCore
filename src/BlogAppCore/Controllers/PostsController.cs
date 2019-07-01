using System.Collections.Generic;
using System.Threading.Tasks;
using BlogAppCore.Application.Posts.Commands.Create;
using BlogAppCore.Application.Posts.Commands.Delete;
using BlogAppCore.Application.Posts.Commands.Update;
using BlogAppCore.Application.Posts.Models;
using BlogAppCore.Application.Posts.Queries.GetPostDetail;
using BlogAppCore.Application.Posts.Queries.GetPostsByCategory;
using BlogAppCore.Application.Posts.Queries.GetPostsByTag;
using BlogAppCore.Application.Posts.Queries.GetPostsPreview;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppCore.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class PostsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostPreviewDto>>> GetList()
        {
            return await Mediator.Send(new GetPostsPreviewQuery());
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<PostDetailDto>> GetBySlug(string slug)
        {
            return await Mediator.Send(new GetPostDetailQuery { Slug = slug });
        }

        [HttpGet("{categorySlug}")]
        public async Task<ActionResult<IEnumerable<PostPreviewDto>>> GetByCategory(string categorySlug)
        {
            return await Mediator.Send(new GetPostsByCategoryQuery { CategorySlug = categorySlug });
        }

        [HttpGet("{tagSlug}")]
        public async Task<ActionResult<IEnumerable<PostPreviewDto>>> GetByTag(string tagSlug)
        {
            return await Mediator.Send(new GetPostsByTagQuery { TagSlug = tagSlug });
        }

        [HttpPost]
        public async Task<ActionResult<PostDetailDto>> Create(CreatePostCommand command)
        {
            var newPost = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetBySlug), new { slug = newPost.Slug }, newPost);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePostCommand { Id = id });

            return NoContent();
        }
    }
}
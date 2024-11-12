using ChitChat.Domain.Entities.PostEntities;

using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public PostController()
        {
            // this._postService = postService;
        }
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            return Ok(new Post() { CreatedOn = DateTime.Now, Description = "Hello World from CI/CD" });
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(new Post() { CreatedOn = DateTime.Now, Description = $"Hello{id}" });
        }
    }
}

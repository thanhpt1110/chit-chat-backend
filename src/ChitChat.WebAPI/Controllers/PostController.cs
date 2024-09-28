using ChitChat.Application.Models;
using ChitChat.Application.Services;
using ChitChat.Domain.Entities.PostEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            return Ok(new Post() { CreatedOn = DateTime.Now, Description = "Hello"} );
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(new Post() { CreatedOn = DateTime.Now, Description = $"Hello{id}" });

        }
    }
}

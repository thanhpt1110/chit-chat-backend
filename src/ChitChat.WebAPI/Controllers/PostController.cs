using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Application.Models.Dtos.Post.Comments;
using ChitChat.Application.Models.Dtos.Post.CreatePost;
using ChitChat.Application.Services.Interface;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChitChat.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            this._postService = postService;
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllPostsAsync([FromQuery] PostSearchQueryDto query)
        {
            return Ok(ApiResult<List<PostDto>>.Success(await _postService.GetAllPostsAsync(query)));
        }
        [HttpGet]
        [Route("{postId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostByIdAsync(Guid postId)
        {
            return Ok(ApiResult<PostDto>.Success(await _postService.GetPostByIdAsync(postId)));
        }
        [HttpGet]
        [Route("reccomendation")]
        public async Task<IActionResult> GetReccomendationPostsAsync([FromQuery] PaginationFilter query)
        {
            return Ok(ApiResult<PostDto>.Success(await _postService.GetReccomendationPostsAsync(query)));
        }
        [HttpGet]
        [Route("{postId}/comments/{commentId}")]
        public async Task<IActionResult> GetAllReplyCommentsAsync(Guid postId, Guid commentId)
        {
            return Ok(ApiResult<List<CommentDto>>.Success(await _postService.GetAllReplyCommentsAsync(postId, commentId)));
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateNewPost([FromForm] CreatePostRequestDto requestDto)
        {
            return Ok(ApiResult<PostDto>.Success(await _postService.CreateNewPostAsync(requestDto)));
        }
        [HttpPost]
        [Route("{postId}/comments")]
        public async Task<IActionResult> CreateNewCommentAsync(Guid postId, [FromBody] CreateCommentRequestDto requestDto)
        {
            return Ok(ApiResult<CommentDto>.Success(await _postService.CreateNewCommentAsync(postId, requestDto)));
        }
        [HttpPost]
        [Route("{postId}/comments/{parentCommentId}")]
        public async Task<IActionResult> CreateReplyCommentAsync(Guid postId, Guid parentCommentId, [FromBody] CreateCommentRequestDto requestDto)
        {
            return Ok(ApiResult<CommentDto>.Success(await _postService.CreateReplyCommentAsync(postId, parentCommentId, requestDto)));
        }
        [HttpPut]
        [Route("{postId}/react")]
        public async Task<IActionResult> ToggleReactPostAsync(Guid postId)
        {
            return Ok(ApiResult<bool>.Success(await _postService.ToggleReactPostAsync(postId)));
        }
        [HttpPut]
        [Route("{postId}")]
        public async Task<IActionResult> UpdatePostByIdAsync([FromBody] PostDto postDto, Guid postId)
        {
            return Ok(ApiResult<PostDto>.Success(await _postService.UpdatePostByIdAsync(postDto, postId)));
        }
        [HttpPut]
        [Route("{postId}/comments/{commentId}")]
        public async Task<IActionResult> UpdateCommentAsync([FromBody] CommentDto commentDto, Guid commentId)
        {
            return Ok(ApiResult<CommentDto>.Success(await _postService.UpdateCommentAsync(commentDto, commentId)));
        }
        [HttpPut]
        [Route("{postId}/comments/{commentId}/react")]
        public async Task<IActionResult> ToggleReactCommentAsync(Guid commentId)
        {
            return Ok(ApiResult<bool>.Success(await _postService.ToggleReactCommentAsync(commentId)));
        }
        [HttpDelete]
        [Route("{postId}")]
        public async Task<IActionResult> DeletePostByIdAsync(Guid postId)
        {
            return Ok(ApiResult<PostDto>.Success(await _postService.DeletePostByIdAsync(postId)));
        }
        [HttpDelete]
        [Route("{postId}/comments/{commentId}")]
        public async Task<IActionResult> DeleteCommentByIdAsync(Guid commentId)
        {
            return Ok(ApiResult<CommentDto>.Success(await _postService.DeleteCommentByIdAsync(commentId)));
        }
    }
}

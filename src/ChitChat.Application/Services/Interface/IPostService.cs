using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Application.Models.Dtos.Post.Comments;
using ChitChat.Application.Models.Dtos.Post.CreatePost;

namespace ChitChat.Application.Services.Interface
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAllPostsAsync(PostUserSearchQueryDto query);
        Task<PostDto> GetPostByIdAsync(Guid postId);
        Task<List<PostDto>> GetReccomendationPostsAsync(PostSearchQueryDto query);
        Task<List<CommentDto>> GetAllReplyCommentsAsync(Guid postId, Guid commentId);
        Task<PostDto> CreateNewPostAsync(CreatePostRequestDto requestDto);
        Task<CommentDto> CreateNewCommentAsync(Guid postId, CommentRequestDto requestDto);
        Task<CommentDto> CreateReplyCommentAsync(Guid postId, Guid parentCommentId, CommentRequestDto requestDto);
        Task<PostDto> UpdatePostByIdAsync(UpdatePostRequestDto postDto, Guid postId);
        Task<bool> ToggleReactPostAsync(Guid postId);
        Task<CommentDto> UpdateCommentAsync(CommentRequestDto commentDto, Guid commentId);
        Task<bool> ToggleReactCommentAsync(Guid commentId);
        Task<PostDto> DeletePostByIdAsync(Guid postId);
        Task<CommentDto> DeleteCommentByIdAsync(Guid commentId);

    }
}

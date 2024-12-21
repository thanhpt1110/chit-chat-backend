using ChitChat.Application.Models.Dtos.User;

namespace ChitChat.Application.Models.Dtos.Post.Comments
{
    public class CommentDto : BaseResponseDto
    {
        public Guid PostId { get; set; }
        public string UserPostedId { get; set; }
        public string Content { get; set; }
        public List<CommentDto> ReplyComments { get; set; }
        public UserDto UserPosted { get; set; }
        public int ReactionCount { get; set; }
    }
}

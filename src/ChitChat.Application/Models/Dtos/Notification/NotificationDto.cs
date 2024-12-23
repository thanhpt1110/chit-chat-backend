using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Application.Models.Dtos.Post.Comments;
using ChitChat.Application.Models.Dtos.User;

namespace ChitChat.Application.Models.Dtos.Notification
{
    public class NotificationDto : BaseResponseDto
    {
        public string Content { get; set; }
        public string Type { get; set; }
        public string Reference { get; set; }
        public string Action { get; set; }
        public string ReceiverUserId { get; set; }
        public PostDto? Post { get; set; } // Navigation property
        public CommentDto? Comment { get; set; } // Navigation property
        public UserDto LastInteractorUser { get; set; } // Navigation property
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}

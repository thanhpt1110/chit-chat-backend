namespace ChitChat.Application.Models.Dtos.Notification
{
    public class CreateCommentNotificationDto : CreateNotificationDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
    }
}

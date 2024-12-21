namespace ChitChat.Application.Models.Dtos.Notification
{
    public class CreatePostNotificationDto : CreateNotificationDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid PostId { get; set; }
    }
}

namespace ChitChat.Domain.Entities.SystemEntities.Notification
{
    public class PostNotification : Notification
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; } // Navigation property
        public PostNotification()
        {
            Type = NotificationType.Post.ToString();
        }
    }
}

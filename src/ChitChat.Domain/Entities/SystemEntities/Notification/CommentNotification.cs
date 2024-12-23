namespace ChitChat.Domain.Entities.SystemEntities.Notification
{
    public class CommentNotification : Notification
    {
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; } // Navigation property
        public CommentNotification()
        {
            Type = NotificationType.Comment.ToString();
        }
    }
}

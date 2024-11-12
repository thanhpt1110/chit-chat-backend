namespace ChitChat.Domain.Entities.SystemEntities
{
    public class Notification:BaseEntity
    {
        public string NotificationDetail { get; set; }
        public string UserId { get; set; }
        public DateTime CreateAt { get; set; }

        public UserApplication User { get; set; } // Navigation property
    }
}

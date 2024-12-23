namespace ChitChat.Domain.Entities.SystemEntities.Notification
{
    public abstract class Notification : BaseAuditedEntity
    {
        public string Type { get; set; }
        public string Action { get; set; }
        public string Reference { get; set; }
        public string LastInteractorUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public UserApplication LastInteractorUser { get; set; } // Navigation property
        public UserApplication ReceiverUser { get; set; } // Navigation property
    }
}

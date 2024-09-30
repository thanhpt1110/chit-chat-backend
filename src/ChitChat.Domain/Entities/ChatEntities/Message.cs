namespace ChitChat.Domain.Entities.ChatEntities
{
    public class Message : BaseAuditedEntity
    {
        public Guid ConversationId { get; set; }
        public string SenderId { get; set; }
        public string MessageText { get; set; }
        public string Status { get; set; }

        public Conversation Conversation { get; set; } // Navigation property
        public UserApplication Sender { get; set; } // Navigation property
    }
}

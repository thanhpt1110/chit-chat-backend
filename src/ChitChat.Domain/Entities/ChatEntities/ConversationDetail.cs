namespace ChitChat.Domain.Entities.ChatEntities
{
    public class ConversationDetail : BaseEntity
    {
        public Guid ConversationId { get; set; }
        public string UserId { get; set; }
        public Conversation Conversation { get; set; }
        public UserApplication User { get; set; }
    }
}

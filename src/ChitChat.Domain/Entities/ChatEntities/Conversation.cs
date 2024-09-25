namespace ChitChat.Domain.Entities.ChatEntities
{
    public class Conversation : BaseAuditedEntity
    {
        public Guid? LastMessageId { get; set; } = null;
        //public string userId1 { get; set; }
        //public string userId2 { get; set; }

        public Message LastMessage { get; set; }
        public bool IsSeen { get; set; } = false;
        //public UserApplication User1 { get; set; }
        //public UserApplication User2 { get; set; }

    }
}

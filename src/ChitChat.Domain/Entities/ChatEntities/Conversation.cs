using ChitChat.Domain.Entities.ChatEntities;

public class Conversation : BaseAuditedEntity
{
    public Guid? LastMessageId { get; set; }
    //public string userId1 { get; set; }
    //public string userId2 { get; set; }

    public Message? LastMessage { get; set; }
    public bool IsSeen { get; set; }
    public int NumOfUser { get; set; }
    public string ConversationType { get; set; }

    public IEnumerable<Message> Messages { get; set; }
    public IEnumerable<ConversationDetail> ConversationDetails { get; set; }
}

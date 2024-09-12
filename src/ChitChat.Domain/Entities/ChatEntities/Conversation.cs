using ChitChat.Domain.Identity;

namespace ChitChat.Domain.Entities.ChatEntities
{
    public class Conversation:BaseEntity
    {
        public Guid LastMessageId { get; set; }
        //public string userId1 { get; set; }
        //public string userId2 { get; set; }

        public DateTime CreatedDate { get; set; }
        public Message LastMessage { get; set; }
        //public UserApplication User1 { get; set; }
        //public UserApplication User2 { get; set; }

    }
}

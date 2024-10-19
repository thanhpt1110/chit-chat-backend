namespace ChitChat.Application.Models.Dtos.Message
{
    public class MessageDto
    {
        Guid Id { get; set; }
        public string MessageText { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string SenderId { get; set; }
        public Guid ConversationId { get; set; }
        public string Status { get; set; }
    }
}

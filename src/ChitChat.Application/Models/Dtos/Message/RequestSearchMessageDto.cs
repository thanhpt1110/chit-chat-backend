namespace ChitChat.Application.Models.Dtos.Message
{
    public class RequestSearchMessageDto
    {
        public string Text { get; set; }
        public Guid ConversationId { get; set; }
    }
}

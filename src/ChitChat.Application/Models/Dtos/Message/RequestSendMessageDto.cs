namespace ChitChat.Application.Models.Dtos.Message
{
    public class RequestSendMessageDto
    {
        public Guid ConversationId { get; set; }
        public string MessageText { get; set; }
    }
}

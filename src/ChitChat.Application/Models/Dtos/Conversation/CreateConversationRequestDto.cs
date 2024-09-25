namespace ChitChat.Application.Models.Dtos.Conversation
{
    public class CreateConversationRequestDto
    {
        public string UserSenderId { get; set; }
        public string UserReceiverId { get; set; }
    }
}

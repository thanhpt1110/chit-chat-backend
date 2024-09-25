using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Models.Dtos.User;

namespace ChitChat.Application.Models.Dtos.Conversation
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public UserDto UserReceiver { get; set; }
        public MessageDto? LastMessage { get; set; }
        public bool IsSeen { get; set; } = false;

    }
}

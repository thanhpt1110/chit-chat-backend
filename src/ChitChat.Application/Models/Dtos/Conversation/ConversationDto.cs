using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Models.Dtos.User;

namespace ChitChat.Application.Models.Dtos.Conversation
{
    public class ConversationDto : BaseResponseDto
    {
        public List<UserDto> UserReceivers { get; set; }
        public MessageDto? LastMessage { get; set; }
        public List<string> UserReceiverIds { get; set; }
        public bool IsSeen { get; set; } = false;
    }
}

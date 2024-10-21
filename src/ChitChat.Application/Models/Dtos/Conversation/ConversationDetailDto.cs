using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Models.Dtos.User;

namespace ChitChat.Application.Models.Dtos.Conversation
{
    public class ConversationDetailDto : BaseResponseDto
    {
        public List<UserDto> UserReceivers { get; set; }
        public List<string> UserReceiverIds { get; set; }
        public List<MessageDto> Messages { get; set; }

    }
}

using ChitChat.Application.Models.Dtos.Message;

namespace ChitChat.Application.Services.Interface
{
    public interface IMessageService
    {
        Task<MessageDto> SendMessage(RequestSendMessageDto request, string senderId);
        Task<List<MessageDto>> GetMessagesByConversationId(Guid conversationId, int pageIndex, int pageSize);
        Task<MessageDto> UpdateMessage(MessageDto message);
        Task<List<MessageDto>> FindMessageWithText(RequestSearchMessageDto searchRequest);
    }
}

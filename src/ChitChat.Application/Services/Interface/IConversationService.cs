using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Models.Dtos.Message;

namespace ChitChat.Application.Services.Interface
{
    public interface IConversationService
    {
        Task<List<ConversationDto>> GetAllConversationsAsync(PaginationFilter query);
        Task<ConversationDetailDto> GetConversationsByIdAsync(Guid conversationId, int messagePageIndex, int messagePageSize);
        // post
        Task<ConversationDto> CreateConversationAsync(List<string> userIds);
        Task<MessageDto> SendMessageAsync(Guid conversationId, RequestSendMessageDto request);
        // Put
        Task<MessageDto> UpdateMessageAsync(MessageDto message);
        Task<ConversationDto> UpdateConversationAsync(ConversationDto conversation);
        // Delete
        Task<ConversationDto> DeleteConversationAsync(Guid conversationId);
        Task<MessageDto> DeleteMessageAsync(Guid messageId);
    }
}

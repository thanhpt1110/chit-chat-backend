using ChitChat.Application.Models.Dtos.Conversation;

namespace ChitChat.Application.Services.Interface
{
    public interface IConversationService
    {
        Task<List<ConversationDto>> GetConversationsByUserId(string userId);
        Task<ConversationDto> CreateNewConversation(string recieverId, string senderId);
        Task<ConversationDto> UpdateConversation(ConversationDto conversation);
        Task<ConversationDto> DeleteConversation(Guid conversationId);
    }
}

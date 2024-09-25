using ChitChat.Application.Models.Dtos.Conversation;

namespace ChitChat.Application.Services.Interface
{
    public interface IConversationService
    {
        Task<List<ConversationDto>> GetConversationsByUserId(string userId);
        Task<ConversationDto> CreateNewConversation(CreateConversationRequestDto request);
    }
}

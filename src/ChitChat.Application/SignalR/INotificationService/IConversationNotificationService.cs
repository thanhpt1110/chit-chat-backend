using ChitChat.Application.Models.Dtos.Conversation;

namespace ChitChat.Application.SignalR.INotification
{
    public interface IConversationNotificationService
    {
        Task UpdateConversation(ConversationDto conversationDto, string userSenderId);
        Task AddConversation(ConversationDto conversationDto, string userSenderId);
        Task DeleteConversation(ConversationDto conversationDto, string userSenderId);

    }
}

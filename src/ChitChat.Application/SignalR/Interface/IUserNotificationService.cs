using ChitChat.Application.Models.Dtos.Conversation;

namespace ChitChat.Application.SignalR.Interface
{
    public interface IUserNotificationService
    {
        Task UpdateConversation(ConversationDto conversation, string userSenderId);
        Task AddConversation(ConversationDto conversation, string userSenderId);
        Task DeleteConversation(ConversationDto conversation, string userSenderId);

    }
}

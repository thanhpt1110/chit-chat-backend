using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Models.Dtos.Notification;

namespace ChitChat.Application.SignalR.Interface
{
    public interface IUserNotificationService
    {
        Task UpdateConversation(ConversationDto conversation, string userSenderId);
        Task AddConversation(ConversationDto conversation, string userSenderId);
        Task DeleteConversation(ConversationDto conversation, string userSenderId);
        Task NewNotification(NotificationDto notificationDto);
        Task UpdateNotification(NotificationDto notificationDto);

    }
}

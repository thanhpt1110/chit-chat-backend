using ChitChat.Application.Models.Dtos.Message;

namespace ChitChat.Application.SignalR.Interface
{
    public interface IConversationNotificationService
    {
        Task SendMessage(MessageDto message);
        Task UpdateMessage(MessageDto message);
        Task DeleteMessage(MessageDto message);
    }
}

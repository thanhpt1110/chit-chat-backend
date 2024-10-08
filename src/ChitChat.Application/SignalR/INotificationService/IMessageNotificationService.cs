using ChitChat.Application.Models.Dtos.Message;

namespace ChitChat.Application.SignalR.INotification
{
    public interface IMessageNotificationService
    {
        Task SendMessageToSpecificClient(MessageDto messageDto);
    }
}

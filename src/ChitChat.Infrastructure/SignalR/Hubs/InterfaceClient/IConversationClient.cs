using ChitChat.Application.Models.Dtos.Message;

namespace ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient
{
    public interface IConversationClient
    {
        Task ConversationJoined(string message);
        Task NewMessage(MessageDto message);
        Task UpdateMessage(MessageDto message);
        Task DeleteMessage(MessageDto message);

    }
}

using ChitChat.Application.Models.Dtos.Message;

namespace ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient
{
    public interface IChatClient
    {
        Task JoinChat(string message);
        Task NewMessage(MessageDto message);
        Task UpdateMessage(MessageDto message);
        Task JoinedConversation(Guid conversationId);
    }
}

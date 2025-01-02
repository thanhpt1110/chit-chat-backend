using ChitChat.Application.Models.Dtos.Message;

namespace ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient
{
    public interface IConversationClient
    {
        Task ConversationJoined(string message);
        Task NewMessage(MessageDto message);
        Task UpdateMessage(MessageDto message);
        Task DeleteMessage(MessageDto message);

        // Calling
        Task ReceiveOffer(string connectionId, string sdp);
        Task ReceiveAnswer(string connectionId, string sdp);
        Task ReceiveIceCandidate(string connectionId, string candidate);
        Task ReceiveCall(Guid ConversationId);
    }
}

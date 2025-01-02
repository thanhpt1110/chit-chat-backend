using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChitChat.Infrastructure.SignalR.Hubs
{
    public class ConversationHub : Hub<IConversationClient>
    {
        private readonly ILogger<ConversationHub> _logger;
        public ConversationHub(ILogger<ConversationHub> logger)
        {
            _logger = logger;
        }
        public override Task OnConnectedAsync() => base.OnConnectedAsync();
        public async Task JoinConversationGroup(Guid conversationId)
        {
            var roomId = HubRoom.ConversationHubJoinRoom(conversationId);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).ConversationJoined($"{Context.ConnectionId} has joined the room {roomId}");
        }
        public async Task SendOffer(Guid conversationId, string sdp)
        {
            var roomId = HubRoom.ConversationHubJoinRoom(conversationId);
            await Clients.OthersInGroup(roomId).ReceiveOffer(Context.ConnectionId, sdp);
        }

        public async Task SendAnswer(Guid conversationId, string sdp)
        {
            var roomId = HubRoom.ConversationHubJoinRoom(conversationId);
            await Clients.OthersInGroup(roomId).ReceiveAnswer(Context.ConnectionId, sdp);
        }

        public async Task SendIceCandidate(Guid conversationId, string candidate)
        {
            var roomId = HubRoom.ConversationHubJoinRoom(conversationId);
            await Clients.OthersInGroup(roomId).ReceiveIceCandidate(Context.ConnectionId, candidate);
        }
        public async Task StartCall(Guid conversationId)
        {
            var roomId = HubRoom.ConversationHubJoinRoom(conversationId);
            await Clients.OthersInGroup(roomId).ReceiveCall(conversationId);
        }

    }
}

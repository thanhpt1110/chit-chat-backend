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
            await Groups.AddToGroupAsync(Context.ConnectionId, HubRoom.ConversationHubJoinRoom(conversationId));
            await Clients.Group(HubRoom.ConversationHubJoinRoom(conversationId)).ConversationJoined($"{Context.ConnectionId} has joined the room {HubRoom.ConversationHubJoinRoom(conversationId)}");
        }
    }
}
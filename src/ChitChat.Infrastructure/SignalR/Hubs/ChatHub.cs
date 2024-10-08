using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChitChat.Infrastructure.SignalR.Hubs
{
    public sealed class ChatHub : Hub<IChatClient>
    {
        ILogger<ChatHub> _logger;
        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"User {Context.ConnectionId} has joined to hubs");
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(MessageDto message)
        {
            await Clients.Group(HubFormat.ChatHubJoinFormat(message.ConversationId)).NewMessage(message);
        }
        public async Task JoinConversation(Guid conversationId)
        {
            _logger.LogInformation($"User {Context.ConnectionId} has joined conversation ${conversationId}");
            await Groups.AddToGroupAsync(Context.ConnectionId, HubFormat.ChatHubJoinFormat(conversationId));
            await Clients.Client(Context.ConnectionId).JoinedConversation(conversationId);
        }
    }
}

using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.SignalR.Interface;
using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs;
using ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient;

using Microsoft.AspNetCore.SignalR;

namespace ChitChat.Infrastructure.SignalR.Services
{
    public class ConversationNotificationService : IConversationNotificationService
    {
        private readonly IHubContext<ConversationHub, IConversationClient> _hubContext;
        public ConversationNotificationService(IHubContext<ConversationHub, IConversationClient> hubContext) => this._hubContext = hubContext;


        public async Task SendMessage(MessageDto message)
        {
            await _hubContext.Clients.Group(HubRoom.ConversationHubJoinRoom(message.ConversationId)).NewMessage(message);
        }

        public async Task UpdateMessage(MessageDto message)
        {
            await _hubContext.Clients.Group(HubRoom.ConversationHubJoinRoom(message.ConversationId)).UpdateMessage(message);
        }
        public async Task DeleteMessage(MessageDto message)
        {
            await _hubContext.Clients.Group(HubRoom.ConversationHubJoinRoom(message.ConversationId)).DeleteMessage(message);
        }
    }
}

using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.SignalR.INotification;
using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs;
using ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient;
using Microsoft.AspNetCore.SignalR;

namespace ChitChat.Infrastructure.SignalR.Services
{
    public class MessageNotificationService : IMessageNotificationService
    {
        private readonly IHubContext<ChatHub, IChatClient> _hubContext;
        public MessageNotificationService(IHubContext<ChatHub, IChatClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessageToSpecificClient(MessageDto messageDto)
        {
            await _hubContext.Clients.Group(HubFormat.ChatHubJoinFormat(messageDto.ConversationId)).NewMessage(messageDto);
        }
    }
}

using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.SignalR.INotification;
using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs;
using ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient;
using Microsoft.AspNetCore.SignalR;

namespace ChitChat.Infrastructure.SignalR.Services
{
    public class ConversationNotificationService : IConversationNotificationService
    {
        private readonly IHubContext<ConversationHub, IConversationClient> _hubContext;
        public ConversationNotificationService(IHubContext<ConversationHub, IConversationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AddConversation(ConversationDto conversationDto, string userSenderId)
        {
            await _hubContext.Clients.Group(HubFormat.ConversationHubJoinFormat(conversationDto.UserReceiverId)).AddNewConversation(conversationDto);
            await _hubContext.Clients.Group(HubFormat.ConversationHubJoinFormat(userSenderId)).AddNewConversation(conversationDto);
        }

        public async Task DeleteConversation(ConversationDto conversationDto, string userSenderId)
        {
            await _hubContext.Clients.Group(HubFormat.ConversationHubJoinFormat(conversationDto.UserReceiverId)).DeleteConversation(conversationDto);
            await _hubContext.Clients.Group(HubFormat.ConversationHubJoinFormat(userSenderId)).DeleteConversation(conversationDto);
        }

        public async Task UpdateConversation(ConversationDto conversationDto, string userSenderId)
        {
            await _hubContext.Clients.Group(HubFormat.ConversationHubJoinFormat(conversationDto.UserReceiverId)).UpdateConverastion(conversationDto);
            await _hubContext.Clients.Group(HubFormat.ConversationHubJoinFormat(userSenderId)).UpdateConverastion(conversationDto);
        }
    }
}

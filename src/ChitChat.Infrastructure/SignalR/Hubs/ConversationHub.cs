using ChitChat.Application.Exceptions;
using ChitChat.Application.Helpers;
using ChitChat.Application.Localization;
using ChitChat.Application.SignalR.IConnectionManager;
using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient;
using Microsoft.AspNetCore.SignalR;

namespace ChitChat.Infrastructure.SignalR.Hubs
{
    public sealed class ConversationHub : Hub<IConversationClient>
    {
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly ITokenService _tokenService;
        public ConversationHub(IUserConnectionManager userConnectionManager, ITokenService tokenService)
        {
            _userConnectionManager = userConnectionManager;
            _tokenService = tokenService;
        }
        public override async Task OnConnectedAsync()
        {
            await this.Clients.All.JoinHub($"{Context.ConnectionId} has joined");
        }
        public async Task JoinConversationHub(string accessToken)
        {
            string userId = await _tokenService.GetUserIdFromAccessToken(accessToken);
            if (userId == null)
            {
                throw new UnauthorizeException(ValidationTexts.UnauthorizedAccess.Format("Conversation Hub socket"));
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, HubFormat.ConversationHubJoinFormat(userId));
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

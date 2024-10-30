using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ChitChat.Infrastructure.SignalR.Hubs
{
    public class UserHub : Hub<IUserClient>
    {
        ILogger<UserHub> _logger;
        public UserHub(ILogger<UserHub> logger)
        {
            _logger = logger;
        }
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"User {Context.ConnectionId} has joined to hubs");
            return base.OnConnectedAsync();
        }
        public async Task JoinUserHub(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, HubRoom.UserHubJoinRoom(userId));
        }
    }

}

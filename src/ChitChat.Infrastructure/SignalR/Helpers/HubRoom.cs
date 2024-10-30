namespace ChitChat.Infrastructure.SignalR.Helpers
{
    public class HubRoom
    {
        public static string ConversationHubJoinRoom(Guid conversationId)
        {
            return $"{HubEnum.Conversation.ToString()}: ${conversationId}";
        }
        public static string UserHubJoinRoom(string userId)
        {
            return $"{HubEnum.User.ToString()}: ${userId}";
        }
    }
}

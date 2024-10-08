namespace ChitChat.Infrastructure.SignalR.Helpers
{
    public static class HubFormat
    {
        public static string ChatHubJoinFormat(Guid conversationId)
        {
            return $"{HubEnum.Conversation.ToString()}: ${conversationId}";
        }
        public static string ConversationHubJoinFormat(string userId)
        {
            return $"{HubEnum.User.ToString()}: ${userId}";
        }
    }
}

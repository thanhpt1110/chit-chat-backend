using ChitChat.Application.Models.Dtos.Conversation;

namespace ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient
{
    public interface IUserClient
    {
        Task UpdateConversation(ConversationDto conversation);
        Task AddConversation(ConversationDto conversation);
        Task DeleteConversation(ConversationDto conversation);
        // Other events
        Task NotifyNewFollower(string followerId, string followedUserId);

    }
}

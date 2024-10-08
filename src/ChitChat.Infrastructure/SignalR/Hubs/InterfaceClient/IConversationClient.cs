using ChitChat.Application.Models.Dtos.Conversation;

namespace ChitChat.Infrastructure.SignalR.Hubs.InterfaceClient
{
    public interface IConversationClient
    {
        Task AddNewConversation(ConversationDto conversationDto);
        Task UpdateConverastion(ConversationDto conversationDto);
        Task DeleteConversation(ConversationDto conversationDto);
        Task JoinHub(string data);

    }
}

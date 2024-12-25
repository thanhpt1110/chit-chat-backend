using ChitChat.DataAccess.Repositories.Interface;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IConversationRepository : IBaseRepository<Conversation>
    {
        Task<List<Conversation>> GetConversationByUserIdAsync(string userId);
        Task<Conversation?> IsConversationExisted(string userSenderId, string userReceiverId);
    }
}

using ChitChat.DataAccess.Repositories.Interrface;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IConversationRepository : IBaseRepository<Conversation>
    {
        Task<List<Conversation>> GetConversationByUserIdAsync(string userId);
        Task<bool> IsConversationExisted(string userSenderId, string userReceiverId);
    }
}

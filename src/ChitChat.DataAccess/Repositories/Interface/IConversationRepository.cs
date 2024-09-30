using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.ChatEntities;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IConversationRepository : IBaseRepository<Conversation>
    {
        Task<List<Conversation>> GetConversationByUserIdAsync(string userId);
        Task<bool> IsConversationExisted(string userSenderId, string userReceiverId);
    }
}

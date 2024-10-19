using ChitChat.DataAccess.Data;
using ChitChat.DataAccess.Repositories.Interface;

namespace ChitChat.DataAccess.Repositories
{
    public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
    {
        public ConversationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Conversation>> GetConversationByUserIdAsync(string userId)
        {
            var listConversation = await (from convDetail in Context.ConversationDetails
                                          join conv in Context.Conversations on convDetail.ConversationId equals conv.Id
                                          where convDetail.UserId == userId && conv.IsDeleted == false
                                          select conv)
                                          .Include(
                                            c => c.ConversationDetails
                                            )
                                          .Include(c => c.LastMessage)
                                          .AsNoTracking()
                                          .ToListAsync();
            return listConversation;
        }

        public async Task<bool> IsConversationExisted(string userSenderId, string userReceiverId)
        {
            bool hasConversation = await Context.ConversationDetails
            .Where(cd2 => cd2.UserId == userReceiverId)
            .Select(cd2 => Context.ConversationDetails
                .Any(cd1 => cd1.UserId == userSenderId && cd1.ConversationId == cd2.ConversationId))
            .AnyAsync(result => result == true);
            return hasConversation;
        }
    }
}

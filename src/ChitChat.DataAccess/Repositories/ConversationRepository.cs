using ChitChat.DataAccess.Data;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Enums;

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

        public async Task<Conversation?> IsConversationExisted(string userSenderId, string userReceiverId)
        {
            Conversation? hasConversation = await Context.ConversationDetails
                .Where(cd2 => cd2.UserId == userReceiverId && cd2.Conversation.ConversationType == ConversationType.Person.ToString())
                .SelectMany(cd2 => Context.ConversationDetails
                .Where(cd1 => cd1.UserId == userSenderId && cd1.ConversationId == cd2.ConversationId))
                .Select(cd1 => cd1.Conversation)
                .SingleOrDefaultAsync();
            if (hasConversation != null)
                hasConversation.LastMessage = await Context.Messages.SingleOrDefaultAsync(p => p.Id == hasConversation.LastMessageId);
            return hasConversation;
        }
    }
}

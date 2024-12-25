using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.UserEntities;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IUserFollowerRepository : IBaseRepository<UserFollower>
    {
        Task<List<UserFollower>> GetRecommendedUsersAsync(string currentUserId);
    }
}

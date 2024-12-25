using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Entities.UserEntities;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IUserFollowerRepository : IBaseRepository<UserFollower>
    {
        Task<List<UserFollower>> GetRecommendedUsersAsync(string currentUserId);
    }
}

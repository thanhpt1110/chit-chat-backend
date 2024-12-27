using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.UserEntities;
using ChitChat.Domain.Identity;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IUserFollowerRepository : IBaseRepository<UserFollower>
    {
        Task<List<UserApplication>> GetRecommendedUsersAsync(string currentUserId, int pageSize);
    }
}

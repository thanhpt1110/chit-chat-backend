using ChitChat.DataAccess.Data;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Entities.UserEntities;

namespace ChitChat.DataAccess.Repositories
{
    public class UserFollowerRepository : BaseRepository<UserFollower>, IUserFollowerRepository
    {
        public UserFollowerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<UserFollower>> GetRecommendedUsersAsync(string currentUserId)
        {
            var followedUsers = await this.Context.UserFollowers
                .Where(uf => uf.UserId == currentUserId)
                .Select(uf => uf.FollowerId)
                .ToListAsync();
            var recommendedUsers = await Context.UserFollowers
                .Where(uf => followedUsers.Contains(uf.UserId) // Người bạn của tôi theo dõi
                             && uf.FollowerId != currentUserId // Không phải chính tôi
                             && !followedUsers.Contains(uf.FollowerId))
                .Include(p => p.User)// Tôi chưa theo dõi họ
                .Distinct() // Loại bỏ trùng lặp
                .ToListAsync();
            return recommendedUsers;
        }
    }
}

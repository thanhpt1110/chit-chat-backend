using ChitChat.DataAccess.Data;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Entities.UserEntities;
using ChitChat.Domain.Identity;

namespace ChitChat.DataAccess.Repositories
{
    public class UserFollowerRepository : BaseRepository<UserFollower>, IUserFollowerRepository
    {
        public UserFollowerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<UserApplication>> GetRecommendedUsersAsync(string currentUserId, int pageSize)
        {
            var followedUsers = await this.Context.UserFollowers
                .Where(uf => uf.UserId == currentUserId)
                .Select(uf => uf.FollowerId)
                .ToListAsync();
            var recommendedUsers = await Context.UserFollowers
                .Where(uf => followedUsers.Contains(uf.UserId) // Người bạn của tôi theo dõi
                             && uf.FollowerId != currentUserId // Không phải chính tôi
                             && !followedUsers.Contains(uf.FollowerId))
                .Include(p => p.User)
                .Select(p => p.User)// Tôi chưa theo dõi họ
                .Distinct() // Loại bỏ trùng lặp
                .ToListAsync();
            var otherFollow = await Context.UserApplications
                .Where(p => p.Id != currentUserId//  Tôi chưa theo dõi họ
                             && !followedUsers.Contains(p.Id))// Họ chưa theo dõi tôi
                .ToListAsync();
            var allUsers = recommendedUsers
            .Concat(otherFollow) // Gộp với otherFollow
            .ToList();
            // Random hóa danh sách
            var randomUsers = allUsers.OrderBy(u => Guid.NewGuid()).ToList();
            return randomUsers.Take(pageSize).ToList();
        }
    }
}

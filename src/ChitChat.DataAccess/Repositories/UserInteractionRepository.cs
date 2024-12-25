using ChitChat.Application.MachineLearning.Models;
using ChitChat.DataAccess.Data;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Entities.SystemEntities;
using ChitChat.Domain.Enums;

namespace ChitChat.DataAccess.Repositories
{
    public class UserInteractionRepository : BaseRepository<UserInteraction>, IUserInteractionRepository
    {
        public UserInteractionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<UserInteractionModelItem>> GetUserInteractionModelForTraining(int pageSize)
        {
            var data = await DbSet
                .OrderByDescending(p => p.InteractionDate)
                .Include(p => p.Post)
                .Take(pageSize)
                .ToListAsync(); // Lấy dữ liệu từ database trước

            var userModelItems = data
                .GroupBy(p => new
                {
                    p.UserId,
                    p.PostId,
                    PostDescription = p.Post.Description
                })
                .Select(g => new UserInteractionModelItem
                {
                    UserId = g.Key.UserId,
                    PostId = g.Key.PostId.ToString(),
                    PostDescription = g.Key.PostDescription,
                    TotalPoint = g.Sum(x => InteractionTypePoint.GetInteractionPoint(x))
                })
                .ToList();
            return userModelItems;
        }
    }
}

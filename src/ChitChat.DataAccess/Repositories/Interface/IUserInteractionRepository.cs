using ChitChat.Application.MachineLearning.Models;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Entities.SystemEntities;

namespace ChitChat.DataAccess.Repositories.Interface
{
    public interface IUserInteractionRepository : IBaseRepository<UserInteraction>
    {
        Task<List<UserInteractionModelItem>> GetUserInteractionModelForTraining(int PageSize);
    }
}

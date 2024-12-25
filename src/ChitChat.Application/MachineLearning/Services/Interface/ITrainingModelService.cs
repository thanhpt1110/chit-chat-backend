using ChitChat.Application.MachineLearning.Models;
using ChitChat.Domain.Entities.PostEntities;

namespace ChitChat.Application.MachineLearning.Services.Interface
{
    public interface ITrainingModelService
    {
        List<ResponseRecommendationModel> GetRecommendationPostModel(string userId, List<UserInteractionModelItem> userInteractions, List<Post> postReccomendation);
    }
}

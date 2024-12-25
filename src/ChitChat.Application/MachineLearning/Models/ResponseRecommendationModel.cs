using ChitChat.Application.Models.Dtos.Post;

namespace ChitChat.Application.MachineLearning.Models
{
    public class ResponseRecommendationModel
    {
        public float Score { get; set; }
        public PostDto? Post { get; set; }
    }
}

using AutoMapper;

using ChitChat.Application.MachineLearning.Models;
using ChitChat.Application.MachineLearning.Services.Interface;
using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Domain.Entities.PostEntities;

using Microsoft.ML;
using Microsoft.ML.Data;

namespace ChitChat.Application.MachineLearning.Services
{
    public class TrainingModelService : ITrainingModelService
    {
        private readonly MLContext _mlContext;
        private readonly IMapper _mapper;
        public TrainingModelService(IMapper mapper)
        {
            _mlContext = new MLContext();
            _mapper = mapper;

        }
        public List<ResponseRecommendationModel> GetRecommendationPostModel(string userId
            , List<UserInteractionModelItem> dataTrain
            , List<Post> postReccomendation)
        {
            IDataView dataTrainView = _mlContext.Data.LoadFromEnumerable(dataTrain);
            var split = _mlContext.Data.TrainTestSplit(dataTrainView, testFraction: 0.2);
            var dataPipeline = _mlContext.Transforms.Conversion.MapValueToKey("UserIdKey", "UserId")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("PostIdKey", "PostId"))
                .Append(_mlContext.Transforms.Text.FeaturizeText("PostDescriptionDecoded", "PostDescription"))
                .Append(_mlContext.Transforms.Conversion.ConvertType("TotalPointDecoded", "TotalPoint", DataKind.Single))
                .Append(_mlContext.Transforms.Concatenate("Features",
                    new[] { "PostDescriptionDecoded", "TotalPointDecoded" }))  // Chúng ta đưa các đặc trưng liên quan vào đây
                .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                    labelColumnName: "TotalPointDecoded",    // Sử dụng ReactionCount làm label (hoặc cái bạn muốn dự đoán)
                    matrixColumnIndexColumnName: "UserIdKey", // Cột này chỉ người dùng
                    matrixRowIndexColumnName: "PostIdKey", // Cột này chỉ bài viết
                    numberOfIterations: 20,    // Số vòng lặp
                    approximationRank: 50));
            var model = dataPipeline.Fit(split.TrainSet);
            var predictions = model.Transform(split.TestSet);
            var metrics = _mlContext.Regression.Evaluate(predictions, labelColumnName: "TotalPointDecoded");
            Console.WriteLine($"R-Squared: {metrics.RSquared:F2}");
            Console.WriteLine($"Root Mean Squared Error: {metrics.RootMeanSquaredError:F2}");
            var predictor = _mlContext.Model.CreatePredictionEngine<UserInteractionModelItem, PostPredictionModel>(model);
            var response = new List<ResponseRecommendationModel>();
            foreach (var item in postReccomendation)
            {
                UserInteractionModelItem userItem = new UserInteractionModelItem
                {
                    UserId = userId,
                    PostId = item.Id.ToString(),
                    PostDescription = item.Description
                };
                var prediction = predictor.Predict(userItem);
                response.Add(new ResponseRecommendationModel()
                {
                    Post = _mapper.Map<PostDto>(item),
                    Score = float.IsNaN(prediction.Score) ? 0 : prediction.Score
                });
            }
            return response;
        }
    }
}

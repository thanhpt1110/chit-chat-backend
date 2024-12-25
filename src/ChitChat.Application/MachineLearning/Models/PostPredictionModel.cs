using Microsoft.ML.Data;

namespace ChitChat.Application.MachineLearning.Models
{
    public class PostPredictionModel
    {
        [ColumnName("Score")]
        public float Score { get; set; }
    }
}

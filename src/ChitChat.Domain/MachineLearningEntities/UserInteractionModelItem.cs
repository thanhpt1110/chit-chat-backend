namespace ChitChat.Application.MachineLearning.Models
{
    public class UserInteractionModelItem
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
        public string PostDescription { get; set; } = string.Empty;
        public float ReactionCount { get; set; }
        public float TotalPoint { get; set; }
    }
}

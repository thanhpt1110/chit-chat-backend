using ChitChat.Domain.Entities.SystemEntities;

namespace ChitChat.Domain.Enums
{
    public enum InteractionType
    {
        Comment = 1,
        View = 2,
        Like = 3,
        Unlike = 4
    }
    public static class InteractionTypePoint
    {
        public static int GetInteractionPoint(UserInteraction userInteraction)
        {
            int basePoint = userInteraction.InteractionType switch
            {
                InteractionType.Comment => 5,
                InteractionType.View => 1,
                InteractionType.Like => 3,
                InteractionType.Unlike => -3,
                _ => 0
            };

            TimeSpan timeElapsed = DateTime.Now - userInteraction.InteractionDate;
            double decayFactor = Math.Max(0.3, 1.0 - timeElapsed.TotalDays / 60); // Giảm dần trong 60 ngày
            return (int)(basePoint * decayFactor);
        }
    }
}

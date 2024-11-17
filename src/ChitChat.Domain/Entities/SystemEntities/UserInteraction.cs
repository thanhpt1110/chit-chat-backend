namespace ChitChat.Domain.Entities.SystemEntities
{
    public class UserInteraction : BaseEntity
    {
        public string UserId { get; set; }
        public Guid PostId { get; set; }
        public InteractionType InteractionType { get; set; }
        public DateTime InteractionDate { get; set; }

        public UserApplication User { get; set; } // Navigation property
        public Post Post { get; set; } // Navigation property
    }
}

namespace ChitChat.Domain.Entities.PostEntities.Reaction
{
    public class ReactionPost : BaseAuditedEntity
    {
        public Guid PostId { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; } // Navigation property
        public UserApplication User { get; set; } // Navigation property
    }
}

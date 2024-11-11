namespace ChitChat.Domain.Entities.PostEntities
{
    public class Post : BaseAuditedEntity
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public int CommentCount { get; set; } = 0;
        public int ReactionCount { get; set; } = 0;
        public UserApplication User { get; set; } // Navigation property
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostDetailTag> PostDetailTags { get; set; }
        public ICollection<PostMedia> PostMedias { get; set; }

    }
}


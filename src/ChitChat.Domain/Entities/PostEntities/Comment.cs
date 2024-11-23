namespace ChitChat.Domain.Entities.PostEntities
{
    public class Comment : BaseAuditedEntity
    {
        public Guid PostId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public string UserPostedId { get; set; }
        public string Content { get; set; }
        public int ReactionCount { get; set; } = 0;
        public string CommentType { get; set; }
        public Post Post { get; set; } // Navigation property
        public Comment ParentComment { get; set; } // Navigation property
        public List<Comment> ReplyComments { get; set; }
        public UserApplication UserPosted { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ChitChat.Domain.Entities.PostEntities
{
    public class Comment : BaseAuditedEntity
    {
        public Guid PostId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public string UserPostedId { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; } // Navigation property
        public Comment ParentComment { get; set; } // Navigation property
        public UserApplication UserPosted { get; set; }

        [Required]
        public string TestMigration { get; set; }
        public string Test_Update_comment { get; set; }
    }
}

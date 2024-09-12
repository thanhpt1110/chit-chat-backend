using chit_chat_backend.Domain.Entities;
using chit_chat_backend.Domain.Identity;

namespace chit_chat_backend.Domain.Entities.PostEntities
{
    public class Post:BaseAuditedEntity
    {
        public string UserId { get; set; }
        public string Description { get; set; }
        public UserApplication User { get; set; } // Navigation property
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostDetailTag> PostDetailTags { get; set; }
        public ICollection<PostMedia> PostMedias { get; set; }

    }
}


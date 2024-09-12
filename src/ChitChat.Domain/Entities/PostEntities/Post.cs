﻿using ChitChat.Domain.Entities;
using ChitChat.Domain.Identity;

namespace ChitChat.Domain.Entities.PostEntities
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

using chit_chat_backend.Domain.Entities;
using chit_chat_backend.Domain.Identity;

namespace chit_chat_backend.Domain.Entities.UserEntities
{
    public class UserFollower:BaseEntity
    {
        public string UserId { get; set; }
        public string FollowerId { get; set; }
        public DateTime FollowedDate { get; set; }

        public UserApplication User { get; set; } // Navigation property
        public UserApplication Follower { get; set; } // Navigation property
    }

}

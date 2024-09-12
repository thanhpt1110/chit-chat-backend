using ChitChat.Domain.Identity;

namespace ChitChat.Domain.Entities.UserEntities
{
    public class UserFollowerRequest:BaseEntity
    {
        public string UserId { get; set; }
        public string FollowerId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }

        public UserApplication User { get; set; } // Navigation property
        public UserApplication Follower { get; set; } // Navigation property
    }
}

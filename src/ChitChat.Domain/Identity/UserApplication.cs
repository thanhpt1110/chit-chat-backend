using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace ChitChat.Domain.Identity
{
    public class UserApplication : IdentityUser
    {
        public DateTime LastLogin { get; set; }
        [MaxLength(255)]
        public string DisplayName { get; set; }
        [MaxLength(255)]
        public string? AvatarUrl { get; set; }
        public UserStatus UserStatus { get; set; }


    }
}

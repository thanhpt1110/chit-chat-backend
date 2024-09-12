using chit_chat_backend.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace chit_chat_backend.Domain.Identity
{
    public class UserApplication : IdentityUser
    {
        public DateTime LastLogin { get; set; }
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string AvatarUrl { get; set; }
        public UserStatus UserStatus { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

    }
}

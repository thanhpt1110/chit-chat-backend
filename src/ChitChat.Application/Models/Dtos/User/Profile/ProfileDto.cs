namespace ChitChat.Application.Models.Dtos.User.Profile
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}

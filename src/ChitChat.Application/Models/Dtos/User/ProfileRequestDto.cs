namespace ChitChat.Application.Models.Dtos.User
{
    public class ProfileRequestDto
    {
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}

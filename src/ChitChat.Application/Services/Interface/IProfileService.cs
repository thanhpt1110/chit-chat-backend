using ChitChat.Application.Models.Dtos.User.Profile;

namespace ChitChat.Application.Services.Interface
{
    public interface IProfileService
    {
        Task<List<ProfileDto>> GetAllProfilesAsync(ProfileSearchQueryDto query);
        Task<ProfileDto> GetProfileByIdAsync(Guid userId);
        Task<ProfileDto> CreatProfileAsync(ProfileRequestDto request);
        Task<ProfileDto> UpdateProfileAsync(Guid userId, ProfileRequestDto request);

    }
}

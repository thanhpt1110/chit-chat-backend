using ChitChat.Application.Models.Dtos.User;

namespace ChitChat.Application.Services.Interface
{
    public interface IProfileService
    {
        Task<List<ProfileDto>> GetAllProfilesAsync(string searchText, int pageIndex, int pageSize);
        Task<ProfileDto> GetProfileByIdAsync(Guid userId);
        Task<ProfileDto> CreatProfileAsync(ProfileRequestDto request);
        Task<ProfileDto> UpdateProfileAsync(Guid userId, ProfileDto request);

    }
}

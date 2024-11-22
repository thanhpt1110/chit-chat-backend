using ChitChat.Application.Models.Dtos.Post;

using Microsoft.AspNetCore.Http;

namespace ChitChat.Application.Services.CloudinaryInterface
{
    public interface ICloudinaryService
    {
        Task<List<PostMediaDto>> PostMediaToCloudAsync(List<IFormFile> files, Guid postId);
    }
}
using System.Diagnostics;

using ChitChat.Application.Helpers;
using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Domain.Enums;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ChitChat.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryService> _logger;
        public CloudinaryService(Cloudinary cloudinary, ILogger<CloudinaryService> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }


        public async Task<List<PostMediaDto>> PostMediaToCloudAsync(List<IFormFile> files, Guid postId)
        {
            var stopwatch = Stopwatch.StartNew();
            var uploadTasks = files.Select(async file =>
            {
                var postMediaId = Guid.NewGuid();
                var uploadResult = await UploadFileToCloudinary(file, postId, postMediaId);
                return uploadResult != null
                    ? new PostMediaDto
                    {
                        MediaType = MediaType.Image.ToString(),
                        MediaUrl = uploadResult.SecureUrl.AbsoluteUri,
                        Description = "Image Description",
                        Id = postMediaId
                    }
                    : null;
            });

            var results = await Task.WhenAll(uploadTasks);
            stopwatch.Stop();
            _logger.LogInformation($"Uploaded {files.Count} files to Cloudinary in {stopwatch.ElapsedMilliseconds}ms");
            return results.Where(result => result != null).ToList();
        }

        private async Task<ImageUploadResult> UploadFileToCloudinary(IFormFile file, Guid postId, Guid postMediaId)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    string customId = GetPublicId(postId, postMediaId);

                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = customId,
                        Transformation = new Transformation()
                                        .Width(800)
                                        .Height(800)
                                        .Crop("limit")
                    };
                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            return uploadResult;
        }
        public async Task<bool> DeleteMediaFromCloudAsync(Guid postId, Guid postMediaId)
        {
            string publicId = GetPublicId(postId, postMediaId);
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);
            return result.Result == "ok";
        }
        private static string GetPublicId(Guid postId, Guid postMediaId)
        {
            return $"post_{postId}/media_{postMediaId}";
        }
    }
}

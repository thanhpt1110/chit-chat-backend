using CloudinaryDotNet;

using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure.CloudinaryConfigurations
{
    public static class CloudinaryRegisteration
    {
        public static void AddCloudinary(this IServiceCollection services)
        {
            CloudinarySetting cloudinarySetting = new CloudinarySetting()
            {
                CloudName = Environment.GetEnvironmentVariable("CloudinaryCloudName"),
                ApiKey = Environment.GetEnvironmentVariable("CloudinaryApiKey"),
                ApiSecret = Environment.GetEnvironmentVariable("CloudinaryApiSecret")
            };
            Account account = new Account(cloudinarySetting.CloudName
                                        , cloudinarySetting.ApiKey
                                        , cloudinarySetting.ApiSecret);
            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton<Cloudinary>(cloudinary);
        }
    }
}

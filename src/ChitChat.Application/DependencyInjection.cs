using System.Reflection;

using ChitChat.Application.MachineLearning.Services;
using ChitChat.Application.MachineLearning.Services.Interface;
using ChitChat.Application.Mapping;
using ChitChat.Application.Services;
using ChitChat.Application.Services.Interface;
using ChitChat.Application.Validators;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
namespace ChitChat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add Validators

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssemblyContaining<IValidatorMarker>();

            // Add MediaR

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // Add AutoMapper

            services.AddAutoMapper(typeof(IMappingProfileMarker));

            // Add Services
            services.AddServices();


            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ITrainingModelService, TrainingModelService>();
            return services;
        }
    }
}

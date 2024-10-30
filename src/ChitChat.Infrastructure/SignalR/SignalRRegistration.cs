using ChitChat.Application.SignalR.Interface;
using ChitChat.Infrastructure.SignalR.Helpers;
using ChitChat.Infrastructure.SignalR.Hubs;
using ChitChat.Infrastructure.SignalR.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ChitChat.Infrastructure.SignalR
{
    public static class SignalRRegistration
    {
        public static WebApplicationBuilder AddSignalRRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSignalRService();
            return builder;
        }
        public static void AddSignalRHub(this IEndpointRouteBuilder route)
        {
            route.MapHub<UserHub>(HubEndpoint.UserHubEndpoint);
            route.MapHub<ConversationHub>(HubEndpoint.ConversationHubEndpoint);
        }
        private static IServiceCollection AddSignalRService(this IServiceCollection services)
        {
            services.AddSignalR();
            /*            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();*/
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IConversationNotificationService, ConversationNotificationService>();
            return services;
        }
    }
}

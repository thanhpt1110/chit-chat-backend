using ChitChat.Application.SignalR.INotification;
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
            route.MapHub<ChatHub>(HubEndpoint.ChatHubEndpoint);
            route.MapHub<ConversationHub>(HubEndpoint.ConversationHubEndpoint);
        }
        private static IServiceCollection AddSignalRService(this IServiceCollection services)
        {
            services.AddSignalR();
            /*            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();*/
            services.AddScoped<IMessageNotificationService, MessageNotificationService>();
            services.AddScoped<IConversationNotificationService, ConversationNotificationService>();
            return services;
        }


    }
}

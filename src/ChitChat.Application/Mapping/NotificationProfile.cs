using AutoMapper;

using ChitChat.Application.Models.Dtos.Notification;
using ChitChat.Domain.Entities.SystemEntities.Notification;

namespace ChitChat.Application.Mapping
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<CommentNotification, NotificationDto>();
            CreateMap<PostNotification, NotificationDto>();
            CreateMap<NotificationDto, CommentNotification>();
            CreateMap<CreateCommentNotificationDto, CommentNotification>().ReverseMap();
            CreateMap<CreatePostNotificationDto, PostNotification>().ReverseMap();
            CreateMap<CreateUserNotificationDto, UserNotification>().ReverseMap();
        }
    }
}

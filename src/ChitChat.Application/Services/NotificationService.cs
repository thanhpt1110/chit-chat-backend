using AutoMapper;

using ChitChat.Application.Helpers;
using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Notification;
using ChitChat.Application.Services.Interface;
using ChitChat.Application.SignalR.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.Domain.Entities.SystemEntities.Notification;

using Microsoft.EntityFrameworkCore;

namespace ChitChat.Application.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly IBaseRepository<UserNotification> _userNotificationRepository;
        private readonly IBaseRepository<CommentNotification> _commentNotificationRepository;
        private readonly IBaseRepository<PostNotification> _postNotificationRepository;
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly IUserNotificationService _userNotificationServices;
        public NotificationService(
            IRepositoryFactory repositoryFactory
            , IClaimService claimService
            , IMapper mapper
            , IUserNotificationService userNotificationService)
        {
            _userNotificationRepository = repositoryFactory.GetRepository<UserNotification>();
            _commentNotificationRepository = repositoryFactory.GetRepository<CommentNotification>();
            _postNotificationRepository = repositoryFactory.GetRepository<PostNotification>();
            _claimService = claimService;
            _mapper = mapper;
            _userNotificationServices = userNotificationService;
        }

        public async Task CreateOrUpdateCommentNotificationAsync(CreateCommentNotificationDto createCommentNotification)
        {
            if (_claimService.GetUserId() == createCommentNotification.ReceiverUserId)
                return;
            var notification = await _commentNotificationRepository.GetFirstOrDefaultAsync(
                p => p.CommentId == createCommentNotification.CommentId
                && p.ReceiverUserId == createCommentNotification.ReceiverUserId
                && p.Action == createCommentNotification.Action);
            if (notification == null)
            {
                notification = _mapper.Map<CommentNotification>(createCommentNotification);
                await _commentNotificationRepository.AddAsync(notification);
                await _userNotificationServices.NewNotification(_mapper.Map<NotificationDto>(notification));
            }
            else
            {
                notification.UpdatedOn = DateTime.Now;
                await _commentNotificationRepository.UpdateAsync(notification);
                await _userNotificationServices.UpdateNotification(_mapper.Map<NotificationDto>(notification));
            }

        }
        public async Task CreateOrUpdatePostNotificationAsync(CreatePostNotificationDto createPostNotificationDto)
        {
            if (_claimService.GetUserId() == createPostNotificationDto.ReceiverUserId)
                return;
            var notification = await _postNotificationRepository.GetFirstOrDefaultAsync(
                p => p.PostId == createPostNotificationDto.PostId
                && p.ReceiverUserId == createPostNotificationDto.ReceiverUserId
                && p.Type == createPostNotificationDto.Type
                && p.Action == createPostNotificationDto.Action);
            if (notification == null)
            {
                notification = _mapper.Map<PostNotification>(createPostNotificationDto);
                await _postNotificationRepository.AddAsync(notification);
                await _userNotificationServices.NewNotification(_mapper.Map<NotificationDto>(notification));
            }
            else
            {
                notification.UpdatedOn = DateTime.Now;
                await _postNotificationRepository.UpdateAsync(notification);
                await _userNotificationServices.UpdateNotification(_mapper.Map<NotificationDto>(notification));
            }
        }
        public async Task CreateOrUpdateUserNotificationAsync(CreateUserNotificationDto createUserNotification)
        {
            if (_claimService.GetUserId() == createUserNotification.ReceiverUserId)
                return;
            var userNotification = await _userNotificationRepository.GetFirstOrDefaultAsync(
                p => p.LastInteractorUserId == createUserNotification.LastInteractorUserId
            && p.ReceiverUserId == createUserNotification.ReceiverUserId
            && p.Type == createUserNotification.Type && p.Action == createUserNotification.Action);
            if (userNotification == null)
            {
                var notification = _mapper.Map<UserNotification>(createUserNotification);
                await _userNotificationRepository.AddAsync(notification);
                await _userNotificationServices.NewNotification(_mapper.Map<NotificationDto>(notification));
            }
            else
            {
                userNotification.UpdatedOn = DateTime.Now;
                await _userNotificationRepository.UpdateAsync(userNotification);
                await _userNotificationServices.UpdateNotification(_mapper.Map<NotificationDto>(userNotification));
            }
        }
        public async Task<List<NotificationDto>> GetAllNotificationsAsync(PaginationFilter filter)
        {
            var userId = _claimService.GetUserId();
            var userNotifications = await _userNotificationRepository.GetAllAsync(p => !p.IsDeleted && p.ReceiverUserId == userId, p => p.OrderByDescending(p => p.UpdatedOn), filter.PageIndex, filter.PageSize, p => p.Include(p => p.LastInteractorUser));
            var commentNotifications = await _commentNotificationRepository.GetAllAsync(p => !p.IsDeleted && p.ReceiverUserId == userId, p => p.OrderByDescending(p => p.UpdatedOn), filter.PageIndex, filter.PageSize, p => p.Include(p => p.LastInteractorUser).Include(p => p.Comment).ThenInclude(p => p.Post));
            var postNotifications = await _postNotificationRepository.GetAllAsync(p => !p.IsDeleted && p.ReceiverUserId == userId, p => p.OrderByDescending(p => p.UpdatedOn), filter.PageIndex, filter.PageSize, p => p.Include(p => p.LastInteractorUser).Include(p => p.Post));
            List<NotificationDto> result = new List<NotificationDto>();
            result.AddRange(_mapper.Map<List<NotificationDto>>(userNotifications.Items));
            result.AddRange(_mapper.Map<List<NotificationDto>>(commentNotifications.Items));
            result.AddRange(_mapper.Map<List<NotificationDto>>(postNotifications.Items));
            return result.OrderByDescending(p => p.UpdatedOn).Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToList();
        }
    }
}

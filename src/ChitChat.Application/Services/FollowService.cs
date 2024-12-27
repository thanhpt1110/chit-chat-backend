using AutoMapper;

using ChitChat.Application.Helpers;
using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Notification;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Application.Models.Dtos.User.Follow;
using ChitChat.Application.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Common;
using ChitChat.Domain.Entities.UserEntities;
using ChitChat.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace ChitChat.Application.Services
{
    internal class FollowService : IFollowService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly INotificationService _notificationService;
        public FollowService(IMapper mapper
            , IClaimService claimService
            , IRepositoryFactory repositoryFactory
            , IUserFollowerRepository userFollowerRepository
            , INotificationService notificationService)
        {
            _claimService = claimService;
            _mapper = mapper;
            _userFollowerRepository = userFollowerRepository;
            _notificationService = notificationService;
        }
        public async Task<List<FollowDto>> GetAllFollowerAsync()
        {
            var currentUserId = _claimService.GetUserId();
            var followers = await _userFollowerRepository.GetAllAsync(
                uf => uf.FollowerId == currentUserId,
                uf => uf.Include(uf => uf.User)
            );

            var followerDtos = new List<FollowDto>();
            foreach (var follower in followers)
            {
                var followDto = new FollowDto
                {
                    Id = follower.Id,
                    UserId = follower.UserId,
                    User = _mapper.Map<UserDto>(follower.User)
                };
                followerDtos.Add(followDto);
            }
            return followerDtos;
        }

        public async Task<List<FollowDto>> GetAllFollowingsAsync()
        {
            var currentUserId = _claimService.GetUserId();
            var followings = await _userFollowerRepository.GetAllAsync(
                uf => uf.UserId == currentUserId,
                uf => uf.Include(uf => uf.Follower)
            );

            var followingDtos = new List<FollowDto>();
            foreach (var following in followings)
            {
                var followDto = new FollowDto
                {
                    Id = following.Id,
                    UserId = following.UserId,
                    User = _mapper.Map<UserDto>(following.Follower)
                };
                followingDtos.Add(followDto);
            }
            return followingDtos;
        }

        public async Task<List<FollowDto>> GetRecommendFollowAsync(PaginationFilter filter)
        {
            var currentUserId = _claimService.GetUserId();
            var followers = await _userFollowerRepository.GetRecommendedUsersAsync(currentUserId, filter.PageSize);
            var followingDtos = new List<FollowDto>();
            foreach (var following in followers)
            {
                var followDto = new FollowDto
                {
                    UserId = following.Id,
                    User = _mapper.Map<UserDto>(following)
                };
                followingDtos.Add(followDto);
            }
            return followingDtos;
        }

        public async Task<FollowDto> ToggleFollowAsync(string otherUserId)
        {
            var currentUserId = _claimService.GetUserId();

            // Kiểm tra xem đã có mối quan hệ theo dõi giữa người dùng hiện tại và người được theo dõi chưa
            var existingFollow = await _userFollowerRepository.GetFirstOrDefaultAsync(
                uf => uf.UserId == currentUserId && uf.FollowerId == otherUserId
            );

            if (existingFollow != null)
            {
                // Nếu đã theo dõi, thực hiện hủy theo dõi
                await _userFollowerRepository.DeleteAsync(existingFollow);

                return new FollowDto
                {
                    Id = existingFollow.Id,
                    UserId = existingFollow.UserId,
                };
            }

            // Nếu chưa theo dõi, thêm một mối quan hệ mới
            var newFollow = new UserFollower
            {
                UserId = currentUserId,
                FollowerId = otherUserId
            };

            await _userFollowerRepository.AddAsync(newFollow);
            await _notificationService.CreateOrUpdateUserNotificationAsync(new CreateUserNotificationDto
            {
                ReceiverUserId = otherUserId,
                LastInteractorUserId = currentUserId,
                Type = NotificationType.User.ToString(),
                Action = NotificationActionEnum.USER_FOLLOW.ToString(),
                Reference = NotificationRefText.UserRef(otherUserId)
            });
            return new FollowDto
            {
                Id = newFollow.Id,
                UserId = newFollow.UserId,
            };
        }

    }
}

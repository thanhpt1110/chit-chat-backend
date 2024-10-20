using AutoMapper;

using ChitChat.Application.Exceptions;
using ChitChat.Application.Helpers;
using ChitChat.Application.Localization;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Application.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.ChatEntities;
using ChitChat.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace ChitChat.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IBaseRepository<ConversationDetail> _conversationDetailRepository;
        private readonly IBaseRepository<Message> _messageRepository;
        private IMapper _mapper;
        private readonly IClaimService _claimService;
        private readonly IUserRepository _userRepository;
        public ConversationService(IConversationRepository conversationRepository
            , IUserRepository userRepository
            , IRepositoryFactory repositoryFactory
            , IMapper mapper
            , IClaimService claimService)
        {
            _userRepository = userRepository;
            _conversationRepository = conversationRepository;
            _conversationDetailRepository = repositoryFactory.GetRepository<ConversationDetail>();
            _messageRepository = repositoryFactory.GetRepository<Message>();
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<List<ConversationDto>> GetConversationsByUserIdAsync(int pageIndex, int pageSize)
        {
            var userId = _claimService.GetUserId();
            var paginationResponse = await _conversationRepository
                                    .GetAllAsync(p => p.ConversationDetails.Any(p => p.UserId == userId)
                                    , p => p.OrderByDescending(c => c.UpdatedOn), pageIndex, pageSize
                                    , query => query.Include(c => c.LastMessage)
                                                    .Include(c => c.ConversationDetails));
            List<ConversationDto> response = new();
            foreach (Conversation c in paginationResponse.Items)
            {
                var receiverIds = c.ConversationDetails.Where(u => u.UserId != userId).Select(p => p.UserId);
                var receiverUsers = await _userRepository.GetAllAsync(p => receiverIds.Contains(p.Id));
                ConversationDto conversationDto = new();
                conversationDto.LastMessage = _mapper.Map<MessageDto>(c.LastMessage);
                conversationDto.IsSeen = c.IsSeen;
                conversationDto.UserReceivers = _mapper.Map<List<UserDto>>(receiverUsers);
                conversationDto.Id = c.Id;
                conversationDto.UserReceiverIds = receiverIds.ToList();
                response.Add(conversationDto);
            }
            return response;
        }
        public async Task<ConversationDetailDto> GetConversationsDetailAsync(Guid conversationId, int messagePageIndex, int messagePageSize)
        {
            var userId = _claimService.GetUserId();
            var conversation = await _conversationRepository
                .GetFirstOrDefaultAsync(p => p.Id == conversationId, query => query
                .Include(c => c.ConversationDetails));
            var receiverIds = conversation.ConversationDetails.Where(u => u.UserId != userId).Select(p => p.UserId);
            var receiverUsers = await _userRepository.GetAllAsync(p => receiverIds.Contains(p.Id));
            ConversationDetailDto response = new();
            response.UserReceivers = _mapper.Map<List<UserDto>>(receiverUsers);
            response.Id = conversation.Id;
            response.UserReceiverIds = receiverIds.ToList();
            var responsePagination = await _messageRepository
                .GetAllAsync(p => p.IsDeleted == false && p.ConversationId == conversationId
                            , p => p.OrderByDescending(p => p.UpdatedOn)
                            , messagePageIndex, messagePageSize
                            );
            response.Messages = _mapper.Map<List<MessageDto>>(responsePagination.Items);
            return response;

        }
        public async Task<ConversationDto> CreateConversationAsync(List<string> userIds)
        {
            if (userIds.Count < 2)
            {
                throw new InvalidModelException(ValidationTexts.NotValidate.Format(userIds.GetType(), userIds));
            }
            if (userIds.Count == 2 && await _conversationRepository.IsConversationExisted(userIds[0], userIds[1]))
            {
                throw new ConflictException(ValidationTexts.Conflict.Format("Conversation", userIds[0] + " and user " + userIds[1]));
            }
            Conversation conversation = new Conversation()
            {
                IsDeleted = false,
                LastMessageId = null,
                NumOfUser = userIds.Count,
                ConversationType = userIds.Count == 2 ? ConversationType.Person.ToString() : ConversationType.Group.ToString(),
            };
            await _conversationRepository.AddAsync(conversation);
            List<ConversationDetail> conversationDetails = new();
            foreach (var user in userIds)
            {
                conversationDetails.Add(new ConversationDetail()
                {
                    ConversationId = conversation.Id,
                    UserId = user,
                });
            }
            await _conversationDetailRepository.AddRangeAsync(conversationDetails);
            var conversationDto = _mapper.Map<ConversationDto>(conversation);
            conversationDto.LastMessage = null;
            conversationDto.UserReceiverIds = userIds;
            conversationDto.UserReceivers = _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync(p => userIds.Contains(p.Id)));
            return conversationDto;
        }
        public async Task<MessageDto> SendMessageAsync(Guid conversationId, RequestSendMessageDto request)
        {
            var senderId = _claimService.GetUserId();
            Message message = _mapper.Map<Message>(request);
            message.SenderId = senderId;
            message.ConversationId = conversationId;
            message.Status = MessageStatus.NORMAL;
            var conversation = await _conversationRepository.GetFirstOrDefaultAsync(p => p.Id == conversationId);
            if (conversation == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format(conversation.GetType(), conversationId));
            }
            await _messageRepository.AddAsync(message);
            conversation.LastMessageId = message.Id;
            await _conversationRepository.UpdateAsync(conversation);
            return _mapper.Map<MessageDto>(message);
        }
        public async Task<MessageDto> UpdateMessageAsync(MessageDto messageDto)
        {
            Message message = await _messageRepository.GetFirstOrDefaultAsync(p => p.Id == messageDto.Id);
            message.MessageText = messageDto.MessageText;
            message.Status = MessageStatus.EDITED;
            await _messageRepository.UpdateAsync(message);
            return _mapper.Map<MessageDto>(message);
        }
        public async Task<ConversationDto> UpdateConversationAsync(ConversationDto conversationDto)
        {
            Conversation conversation = await _conversationRepository.GetFirstOrDefaultAsync(p => p.Id == conversationDto.Id);
            if (conversation == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format("Conversation", conversationDto.Id));
            }
            // Cập nhật từng thuộc tính từ DTO
            _mapper.Map(conversationDto, conversation);
            await _conversationRepository.UpdateAsync(conversation);
            return _mapper.Map<ConversationDto>(conversation);
        }
        public async Task<ConversationDto> DeleteConversationAsync(Guid conversationId)
        {
            Conversation conversation = await _conversationRepository.GetFirstOrDefaultAsync(p => p.Id == conversationId);
            conversation.IsDeleted = true;
            await _conversationRepository.UpdateAsync(conversation);
            return _mapper.Map<ConversationDto>(conversation);
        }
        public async Task<MessageDto> DeleteMessageAsync(Guid messageId)
        {
            Message message = await _messageRepository.GetFirstOrDefaultAsync(p => p.Id == messageId);
            message.Status = MessageStatus.UNSENT;
            await _messageRepository.UpdateAsync(message);
            return _mapper.Map<MessageDto>(message);
        }

    }
}

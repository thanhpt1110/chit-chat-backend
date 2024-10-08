using AutoMapper;
using ChitChat.Application.Exceptions;
using ChitChat.Application.Localization;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Application.Services.Interface;
using ChitChat.Application.SignalR.INotification;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.ChatEntities;

namespace ChitChat.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IBaseRepository<ConversationDetail> _conversationDetailRepository;
        private IMapper _mapper;

        private readonly IUserRepository _userRepository;
        private readonly IConversationNotificationService _conversationNotificationService;
        public ConversationService(IConversationRepository conversationRepository,
            IUserRepository userRepository,
            IRepositoryFactory repositoryFactory, IMapper mapper,
            IConversationNotificationService conversationNotificationService)
        {
            _userRepository = userRepository;
            _conversationRepository = conversationRepository;
            _conversationDetailRepository = repositoryFactory.GetRepository<ConversationDetail>();
            _conversationNotificationService = conversationNotificationService;
            _mapper = mapper;
        }
        public async Task<ConversationDto> CreateNewConversation(string recieverId, string senderId)
        {
            if (string.IsNullOrWhiteSpace(recieverId))
            {
                throw new InvalidModelException(ValidationTexts.NotValidate.Format("User Reciver Id ", recieverId));
            }
            if (recieverId == senderId)
            {
                throw new InvalidModelException(ValidationTexts.NotValidate.Format("User Reciver Id ", recieverId));
            }
            var userReciever = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == senderId);
            // Check user existed
            if (userReciever == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format("User Receiver Id", recieverId));
            }
            if (await _userRepository.GetFirstOrDefaultAsync(u => u.Id == senderId) == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format("User Sender Id", senderId));
            }

            if (await _conversationRepository.IsConversationExisted(senderId, recieverId))
            {
                throw new ConflictException(ValidationTexts.Conflict.Format("Conversation", senderId + " and user " + recieverId));
            }
            Conversation conversation = new Conversation()
            {
                IsDeleted = false,
                LastMessageId = null,
            };
            await _conversationRepository.AddAsync(conversation);
            ConversationDetail conversationDetailSender = new ConversationDetail()
            {
                ConversationId = conversation.Id,
                UserId = senderId,
            };
            await _conversationDetailRepository.AddAsync(conversationDetailSender);
            ConversationDetail conversationDetailReceiver = new ConversationDetail()
            {
                ConversationId = conversation.Id,
                UserId = recieverId,
            };
            await _conversationDetailRepository.AddAsync(conversationDetailReceiver);
            ConversationDto conversationDto = new ConversationDto()
            {
                Id = conversation.Id,
                IsSeen = conversation.IsSeen,
                UserReceiverId = userReciever.Id,
                LastMessage = null,
                UserReceiver = _mapper.Map<UserDto>(userReciever)
            };
            await _conversationNotificationService.AddConversation(conversationDto, senderId);
            return conversationDto;
        }

        public async Task<List<ConversationDto>> GetConversationsByUserId(string userId)
        {
            List<Conversation> conversations = await _conversationRepository.GetConversationByUserIdAsync(userId);
            List<ConversationDto> response = new List<ConversationDto>();
            foreach (Conversation c in conversations)
            {
                var receiverId = c.ConversationDetails.FirstOrDefault(u => u.UserId != userId).UserId;
                var receiverUser = await _userRepository.GetFirstOrDefaultAsync(p => p.Id == receiverId);
                ConversationDto conversationDto = new();
                conversationDto.LastMessage = _mapper.Map<MessageDto>(c.LastMessage);
                conversationDto.IsSeen = c.IsSeen;
                conversationDto.UserReceiver = _mapper.Map<UserDto>(receiverUser);
                conversationDto.Id = c.Id;
                conversationDto.UserReceiverId = conversationDto.UserReceiver.Id;
                response.Add(conversationDto);
            }
            return response;
        }

        public async Task<ConversationDto> UpdateConversation(ConversationDto conversation, string senderId)
        {
            await _conversationRepository.UpdateAsync(_mapper.Map<Conversation>(conversation));
            await _conversationNotificationService.UpdateConversation(conversation, senderId);
            return conversation;
        }

        public async Task<ConversationDto> DeleteConversation(Guid conversationId, string senderId)
        {
            Conversation conversation = await _conversationRepository.GetFirstOrDefaultAsync(p => p.Id == conversationId);
            conversation.IsDeleted = true;
            await _conversationRepository.UpdateAsync(conversation);
            var userReciever = conversation.ConversationDetails.Where(p => p.UserId != senderId).FirstOrDefault();
            ConversationDto conversationDto = new ConversationDto()
            {
                Id = conversation.Id,
                IsSeen = conversation.IsSeen,
                UserReceiverId = userReciever.UserId,
                LastMessage = null,
                UserReceiver = null
            };
            await _conversationNotificationService.DeleteConversation(conversationDto, senderId);

            return _mapper.Map<ConversationDto>(conversation);
        }
    }
}

using AutoMapper;
using ChitChat.Application.Exceptions;
using ChitChat.Application.Localization;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Application.Services.Interface;
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
        public ConversationService(IConversationRepository conversationRepository, IUserRepository userRepository, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _userRepository = userRepository;
            _conversationRepository = conversationRepository;
            _conversationDetailRepository = repositoryFactory.GetRepository<ConversationDetail>();
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
                LastMessage = null,
                UserReceiver = _mapper.Map<UserDto>(userReciever)
            };
            return conversationDto;
        }

        public async Task<List<ConversationDto>> GetConversationsByUserId(string userId)
        {
            List<Conversation> conversations = await _conversationRepository.GetConversationByUserIdAsync(userId);
            List<ConversationDto> response = new List<ConversationDto>();
            conversations.ForEach(c =>
            {
                ConversationDto conversationDto = new();
                conversationDto.LastMessage = _mapper.Map<MessageDto>(c.LastMessage);
                conversationDto.IsSeen = c.IsSeen;
                conversationDto.UserReceiver = new UserDto() { Id = c.ConversationDetails.FirstOrDefault(u => u.UserId != userId).UserId };
                conversationDto.Id = c.Id;
                response.Add(conversationDto);
            });
            return response;
        }

        public async Task<ConversationDto> UpdateConversation(ConversationDto conversation)
        {
            await _conversationRepository.UpdateAsync(_mapper.Map<Conversation>(conversation));
            return conversation;
        }

        public async Task<ConversationDto> DeleteConversation(Guid conversationId)
        {
            Conversation conversation = await _conversationRepository.GetFirstOrDefaultAsync(p => p.Id == conversationId);
            conversation.IsDeleted = true;
            await _conversationRepository.UpdateAsync(conversation);
            return _mapper.Map<ConversationDto>(conversation);
        }
    }
}

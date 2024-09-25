using AutoMapper;
using ChitChat.Application.Exceptions;
using ChitChat.Application.Localization;
using ChitChat.Application.Models.Dtos.Conversation;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Application.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.ChatEntities;

namespace ChitChat.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IBaseRepository<Conversation> _conversationRepository;
        private readonly IBaseRepository<ConversationDetail> _conversationDetailRepository;
        private IMapper _mapper;

        private readonly IUserRepository _userRepository;
        public ConversationService(IUserRepository userRepository, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _userRepository = userRepository;
            _conversationRepository = repositoryFactory.GetRepository<Conversation>();
            _conversationDetailRepository = repositoryFactory.GetRepository<ConversationDetail>();
            _mapper = mapper;
        }
        public async Task<ConversationDto> CreateNewConversation(CreateConversationRequestDto request)
        {
            var userReciever = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == request.UserReceiverId);
            if (userReciever == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format("User Receiver Id", request.UserReceiverId));
            }
            if (await _userRepository.GetFirstOrDefaultAsync(u => u.Id == request.UserSenderId) == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format("User Sender Id", request.UserSenderId));
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
                userId = request.UserSenderId,
            };
            await _conversationDetailRepository.AddAsync(conversationDetailSender);
            ConversationDetail conversationDetailReceiver = new ConversationDetail()
            {
                ConversationId = conversation.Id,
                userId = request.UserReceiverId,
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

            throw new NotImplementedException();
        }
    }
}

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
using ChitChat.Domain.Enums;

namespace ChitChat.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBaseRepository<Message> _messageRepository;
        private readonly IBaseRepository<ConversationDetail> _conversationDetailRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConversationNotificationService _conversationNotificationService;
        private readonly IMapper _mapper;
        private readonly IMessageNotificationService _messageNotificationService;
        public MessageService(IRepositoryFactory factoryRepository, IMapper mapper
            , IConversationRepository conversationRepository
            , IConversationNotificationService conversationNotificationService
            , IUserRepository userRepository
            , IMessageNotificationService messageNotificationService)
        {
            _messageRepository = factoryRepository.GetRepository<Message>();
            _mapper = mapper;
            _conversationRepository = conversationRepository;
            _conversationNotificationService = conversationNotificationService;
            _conversationDetailRepository = factoryRepository.GetRepository<ConversationDetail>();
            _userRepository = userRepository;
            _messageNotificationService = messageNotificationService;
        }

        public async Task<List<MessageDto>> FindMessageWithText(RequestSearchMessageDto searchRequest)
        {
            List<Message> messagesFind = await _messageRepository.GetAllAsync(p => p.MessageText.Contains(searchRequest.Text)
            && !p.IsDeleted && p.ConversationId == searchRequest.ConversationId);
            return _mapper.Map<List<MessageDto>>(messagesFind);
        }

        public async Task<List<MessageDto>> GetMessagesByConversationId(Guid conversationId, int pageIndex, int pageSize)
        {
            var paginationResponseMessages = await _messageRepository.GetAllAsync(p => p.ConversationId == conversationId && !p.IsDeleted, p => p.OrderByDescending(c => c.CreatedOn), pageIndex, pageSize);

            return _mapper.Map<List<MessageDto>>(paginationResponseMessages.Items);
        }

        public async Task<MessageDto> SendMessage(RequestSendMessageDto request, string senderId)
        {
            Message message = _mapper.Map<Message>(request);
            message.SenderId = senderId;
            message.Status = MessageStatus.NORMAL;
            var conversation = await _conversationRepository.GetFirstOrDefaultAsync(p => p.Id == request.ConversationId);
            if (conversation == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format(conversation.GetType(), request.ConversationId));
            }
            await _messageRepository.AddAsync(message);
            conversation.LastMessageId = message.Id;
            var userReceiverConversation = await _conversationDetailRepository.GetFirstOrDefaultAsync(p => p.ConversationId == conversation.Id && p.UserId != senderId);
            var userReciver = await _userRepository.GetFirstOrDefaultAsync(p => p.Id == userReceiverConversation.UserId);
            ConversationDto conversationDto = new ConversationDto()
            {
                Id = conversation.Id,
                IsSeen = conversation.IsSeen,
                UserReceiverId = userReceiverConversation.UserId,
                LastMessage = _mapper.Map<MessageDto>(message),
                UserReceiver = _mapper.Map<UserDto>(userReciver)
            };
            var messageDto = _mapper.Map<MessageDto>(message);
            await _messageNotificationService.SendMessageToSpecificClient(messageDto);
            await _conversationRepository.UpdateAsync(conversation);
            await _conversationNotificationService.UpdateConversation(conversationDto, senderId);
            return messageDto;
        }

        public async Task<MessageDto> UpdateMessage(MessageDto message)
        {
            await _messageRepository.UpdateAsync(_mapper.Map<Message>(message));
            return message;
        }
    }
}

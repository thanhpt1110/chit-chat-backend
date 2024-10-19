using AutoMapper;
using ChitChat.Application.Exceptions;
using ChitChat.Application.Localization;
using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Application.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.ChatEntities;
using ChitChat.Domain.Enums;

namespace ChitChat.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBaseRepository<Message> _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IMapper _mapper;
        public MessageService(IBaseRepository<Message> messageRepository, IMapper mapper
            , IConversationRepository conversationRepository)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _conversationRepository = conversationRepository;
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
            await _conversationRepository.UpdateAsync(conversation);
            return _mapper.Map<MessageDto>(message);
        }

        public async Task<MessageDto> UpdateMessage(MessageDto message)
        {
            await _messageRepository.UpdateAsync(_mapper.Map<Message>(message));
            return message;
        }
    }
}

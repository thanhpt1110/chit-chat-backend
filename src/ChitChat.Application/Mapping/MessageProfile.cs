using AutoMapper;
using ChitChat.Application.Models.Dtos.Message;
using ChitChat.Domain.Entities.ChatEntities;

namespace ChitChat.Application.Mapping
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<RequestSendMessageDto, Message>();
            CreateMap<Message, MessageDto>();
        }
    }
}

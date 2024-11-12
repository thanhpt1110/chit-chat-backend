using AutoMapper;

using ChitChat.Application.Models.Dtos.Message;

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

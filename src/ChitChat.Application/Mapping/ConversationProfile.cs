using AutoMapper;

using ChitChat.Application.Models.Dtos.Conversation;

namespace ChitChat.Application.Mapping
{
    public class ConversationProfile : Profile
    {
        public ConversationProfile()
        {
            CreateMap<Conversation, ConversationDto>();
            CreateMap<Conversation, ConversationDetailDto>();
        }
    }
}

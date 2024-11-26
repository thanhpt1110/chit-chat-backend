using AutoMapper;

using ChitChat.Application.Models.Dtos.Post.Comments;
using ChitChat.Domain.Entities.PostEntities;

namespace ChitChat.Application.Mapping
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentRequestDto, Comment>();
        }
    }
}

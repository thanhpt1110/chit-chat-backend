using AutoMapper;

using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Application.Models.Dtos.Post.CreatePost;
using ChitChat.Domain.Entities.PostEntities;

namespace ChitChat.Application.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<CreatePostRequestDto, Post>();
            CreateMap<CreatePostMediaRequestDto, PostMedia>();
            CreateMap<PostMedia, PostMediaDto>();
            //

        }
    }
}

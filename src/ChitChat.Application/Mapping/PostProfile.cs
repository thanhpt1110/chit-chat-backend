using AutoMapper;

using ChitChat.Application.MachineLearning.Models;
using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Application.Models.Dtos.Post.CreatePost;
using ChitChat.Domain.Entities.PostEntities;

namespace ChitChat.Application.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>().ForMember(dest => dest.UserPosted, opt => opt.MapFrom(src => src.User));
            CreateMap<CreatePostRequestDto, Post>();
            CreateMap<UpdatePostRequestDto, Post>();
            CreateMap<CreatePostMediaRequestDto, PostMedia>();
            CreateMap<PostMedia, PostMediaDto>();
            CreateMap<PostMediaDto, PostMedia>();
            CreateMap<ResponseRecommendationModel, PostDto>()
                .ConvertUsing(src => src.Post);

            //

        }
    }
}

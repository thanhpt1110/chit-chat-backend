using System.Linq.Expressions;

using AutoMapper;

using ChitChat.Application.Exceptions;
using ChitChat.Application.Helpers;
using ChitChat.Application.Localization;
using ChitChat.Application.Models;
using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Application.Models.Dtos.Post.Comments;
using ChitChat.Application.Models.Dtos.Post.CreatePost;
using ChitChat.Application.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.PostEntities;
using ChitChat.Domain.Entities.PostEntities.Reaction;
using ChitChat.Domain.Entities.SystemEntities;
using ChitChat.Domain.Enums;
using ChitChat.Domain.Extensions;

using Microsoft.EntityFrameworkCore;

namespace ChitChat.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IClaimService _claimService;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Comment> _commentRepository;
        private readonly IBaseRepository<Post> _postRepository;
        private readonly IBaseRepository<UserInteraction> _userInteractionRepository;
        private readonly IBaseRepository<PostMedia> _postMediaRepository;
        private readonly IBaseRepository<ReactionPost> _reactionPostRepository;
        private readonly IBaseRepository<ReactionComment> _reactionCommentRepository;

        private readonly IUserRepository _userRepository;
        public PostService(
            IClaimService claimService
            , IMapper mapper
            , IRepositoryFactory repositoryFactory
            , IUserRepository userRepository)
        {
            _claimService = claimService;
            _mapper = mapper;
            _userRepository = userRepository;
            _postRepository = repositoryFactory.GetRepository<Post>();
            _commentRepository = repositoryFactory.GetRepository<Comment>();
            _userInteractionRepository = repositoryFactory.GetRepository<UserInteraction>();
            _postMediaRepository = repositoryFactory.GetRepository<PostMedia>();
            _reactionPostRepository = repositoryFactory.GetRepository<ReactionPost>();
            _reactionCommentRepository = repositoryFactory.GetRepository<ReactionComment>();
        }
        #region GET
        public async Task<List<PostDto>> GetAllPostsAsync(PostSearchQueryDto queryDto)
        {
            Expression<Func<Post, bool>> filter = x => !x.IsDeleted;
            var userId = _claimService.GetUserId();
            if (!string.IsNullOrEmpty(queryDto.SearchText))
            {
                filter = filter.And(x => x.Description.Contains(queryDto.SearchText));
            }
            if (!string.IsNullOrEmpty(queryDto.UserId))
            {
                var user = await _userRepository.GetFirstAsync(p => p.Id == queryDto.UserId);
                if (user == null)
                {
                    throw new NotFoundException(ValidationTexts.NotFound.Format("User", queryDto.UserId));
                }
                filter = filter.And(x => x.UserId == queryDto.UserId);
            }
            else
                filter = filter.And(x => x.UserId == userId);
            var paginationResponse = await _postRepository.GetAllAsync(filter, p => p.OrderByDescending(p => p.CreatedOn)
                                                            , queryDto.PageIndex
                                                            , queryDto.PageSize,
                                                            p => p.IgnoreAutoIncludes().Include(c => c.PostMedias)
                                                            );
            return _mapper.Map<List<PostDto>>(paginationResponse.Items);
        }
        public async Task<PostDto> GetPostByIdAsync(Guid postId)
        {
            var post = await _postRepository.GetFirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted
                                                                    , p => p.Include(c => c.PostMedias).Include(c => c.Comments));
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            post.Comments = post.Comments.Where(p => p.CommentType == CommentType.Parent).ToList();
            return _mapper.Map<PostDto>(post);
        }
        public Task<PostDto> GetReccomendationPostsAsync(PaginationFilter query)
        {
            throw new NotImplementedException();
        }
        public async Task<List<CommentDto>> GetAllReplyCommentsAsync(Guid postId, Guid commentId)
        {
            var commentParent = _commentRepository.GetFirstOrDefaultAsync(p => p.Id == commentId && p.CommentType == CommentType.Parent);
            if (commentParent == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Comment", commentId));
            var comments = await _commentRepository.GetAllAsync(p => p.PostId == postId && p.ParentCommentId == commentId);
            return _mapper.Map<List<CommentDto>>(comments);
        }
        #endregion
        #region POST
        public async Task<PostDto> CreateNewPostAsync(CreatePostRequestDto requestDto)
        {
            string userId = _claimService.GetUserId();
            Post post = _mapper.Map<Post>(requestDto);
            post.UserId = userId;
            await _postRepository.AddAsync(post);
            if (post.PostMedias != null)
            {
                foreach (var postMedia in post.PostMedias)
                {
                    postMedia.PostId = post.Id;
                }
                await _postMediaRepository.AddRangeAsync(post.PostMedias);
            }
            return _mapper.Map<PostDto>(post);
        }
        public async Task<CommentDto> CreateNewCommentAsync(Guid postId, CreateCommentRequestDto requestDto)
        {
            var userId = _claimService.GetUserId();
            var comment = _mapper.Map<Comment>(requestDto);
            var post = await _postRepository.GetFirstAsync(p => p.Id == postId);
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            UserInteraction userInteraction = new UserInteraction
            {
                UserId = userId,
                PostId = postId,
                InteractionType = InteractionType.Comment
            };
            comment.PostId = postId;
            comment.UserPostedId = userId;
            comment.CommentType = CommentType.Parent;
            ++post.CommentCount;
            await _commentRepository.AddAsync(comment);
            await _postRepository.UpdateAsync(post);
            await _userInteractionRepository.AddAsync(userInteraction);
            return _mapper.Map<CommentDto>(comment);
        }
        public async Task<CommentDto> CreateReplyCommentAsync(Guid postId, Guid parentCommentId, CreateCommentRequestDto requestDto)
        {
            if (!(await _commentRepository.AnyAsync(p => p.Id == parentCommentId && p.CommentType == CommentType.Parent)))
                throw new NotFoundException(ValidationTexts.NotFound.Format("Comment", parentCommentId));
            var userId = _claimService.GetUserId();
            var comment = _mapper.Map<Comment>(requestDto);
            comment.CommentType = CommentType.Reply;
            var post = await _postRepository.GetFirstAsync(p => p.Id == postId);
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            UserInteraction userInteraction = new UserInteraction
            {
                UserId = userId,
                PostId = postId,
                InteractionType = InteractionType.Comment
            };
            comment.PostId = postId;
            comment.UserPostedId = userId;
            comment.ParentCommentId = parentCommentId;
            ++post.CommentCount;
            await _commentRepository.AddAsync(comment);
            await _userInteractionRepository.AddAsync(userInteraction);
            await _postRepository.UpdateAsync(post);
            return _mapper.Map<CommentDto>(comment);
        }
        #endregion
        #region PUT
        public async Task<PostDto> UpdatePostByIdAsync(PostDto postDto, Guid postId)
        {
            Post post = await _postRepository.GetFirstAsync(p => p.Id == postId);
            if (post.Id != postId)
                throw new InvalidModelException(ValidationTexts.NotValidate.Format("Post", postId));
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            post = _mapper.Map(postDto, post);
            await _postRepository.UpdateAsync(post);
            return _mapper.Map<PostDto>(post);
        }
        public async Task<bool> ToggleReactPostAsync(Guid postId)
        {
            var userId = _claimService.GetUserId();
            var post = await _postRepository.GetFirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            var reactionPost = await _reactionPostRepository.GetFirstOrDefaultAsync(p => p.PostId == postId && p.UserId == userId);
            UserInteraction userInteraction = await _userInteractionRepository.GetFirstOrDefaultAsync(p => p.PostId == postId && p.UserId == userId);
            if (reactionPost == null)
            {
                reactionPost = new ReactionPost
                {
                    PostId = postId,
                    UserId = userId,
                };
                if (userInteraction == null)
                {
                    userInteraction = new UserInteraction
                    {
                        UserId = userId,
                        PostId = postId,
                        InteractionType = InteractionType.Like
                    };
                    await _userInteractionRepository.AddAsync(userInteraction);
                }
                await _reactionPostRepository.AddAsync(reactionPost);
                ++post.ReactionCount;
                await _postRepository.UpdateAsync(post);
                return true;
            }
            else
            {
                if (userInteraction != null)
                {
                    await _userInteractionRepository.DeleteAsync(userInteraction);
                }
                await _reactionPostRepository.DeleteAsync(reactionPost);
                --post.ReactionCount;
                await _postRepository.UpdateAsync(post);
                return false;
            }
        }
        public async Task<CommentDto> UpdateCommentAsync(CommentDto commentDto, Guid commentId)
        {
            var comment = await _commentRepository.GetFirstOrDefaultAsync(p => p.Id == commentId);
            if (comment == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Comment", commentId));
            comment = _mapper.Map(commentDto, comment);
            await _commentRepository.UpdateAsync(comment);
            return _mapper.Map<CommentDto>(comment);
        }
        public async Task<bool> ToggleReactCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetFirstOrDefaultAsync(p => p.Id == commentId);
            if (comment == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format("Comment", commentId));
            }

            var reactionComment = await _reactionCommentRepository.GetFirstOrDefaultAsync(p => p.CommentId == commentId);
            if (reactionComment == null)
            {
                reactionComment = new ReactionComment
                {
                    CommentId = commentId,
                    UserId = _claimService.GetUserId()
                };
                comment.ReactionCount++;
                await _commentRepository.UpdateAsync(comment);
                await _reactionCommentRepository.AddAsync(reactionComment);
                return true;
            }
            else
            {
                comment.ReactionCount--;
                await _commentRepository.UpdateAsync(comment);
                await _reactionCommentRepository.DeleteAsync(reactionComment);
                return false;
            }
        }

        #endregion
        #region DELETE
        public async Task<PostDto> DeletePostByIdAsync(Guid postId)
        {
            Post post = await _postRepository.GetFirstAsync(p => p.Id == postId);
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            var postMedia = await _postMediaRepository.GetAllAsync(p => p.PostId == postId);
            if (postMedia != null)
            {
                foreach (var media in postMedia)
                {
                    await _postMediaRepository.DeleteAsync(media);
                }
            }
            await _postRepository.DeleteAsync(post);
            return _mapper.Map<PostDto>(post);
        }
        public async Task<CommentDto> DeleteCommentByIdAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetFirstOrDefaultAsync(p => p.Id == commentId);
            if (comment == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Comment", commentId));
            await _commentRepository.DeleteAsync(comment);
            return _mapper.Map<CommentDto>(comment);
        }
        #endregion
    }
}


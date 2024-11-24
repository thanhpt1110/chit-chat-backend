using System.Linq.Expressions;

using AutoMapper;

using ChitChat.Application.Exceptions;
using ChitChat.Application.Helpers;
using ChitChat.Application.Localization;
using ChitChat.Application.Models.Dtos.Post;
using ChitChat.Application.Models.Dtos.Post.Comments;
using ChitChat.Application.Models.Dtos.Post.CreatePost;
using ChitChat.Application.Services.CloudinaryInterface;
using ChitChat.Application.Services.Interface;
using ChitChat.DataAccess.Repositories.Interface;
using ChitChat.DataAccess.Repositories.Interrface;
using ChitChat.Domain.Entities.PostEntities;
using ChitChat.Domain.Entities.PostEntities.Reaction;
using ChitChat.Domain.Entities.SystemEntities;
using ChitChat.Domain.Enums;

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
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IUserRepository _userRepository;
        public PostService(
            IClaimService claimService
            , IMapper mapper
            , IRepositoryFactory repositoryFactory
            , IUserRepository userRepository
            , ICloudinaryService cloudinaryService)
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
            _cloudinaryService = cloudinaryService;
        }
        #region GET
        public async Task<List<PostDto>> GetAllPostsAsync(PostUserSearchQueryDto query)
        {
            Expression<Func<Post, bool>> filter = x => !x.IsDeleted;
            var userId = query.UserId;
            if (string.IsNullOrEmpty(userId))
                userId = _claimService.GetUserId();
            var user = await _userRepository.GetFirstAsync(p => p.Id == userId);
            if (user == null)
            {
                throw new NotFoundException(ValidationTexts.NotFound.Format("User", userId));
            }

            var paginationResponse = await _postRepository.GetAllAsync(filter, p => p.OrderByDescending(p => p.CreatedOn)
                                                            , query.PageIndex
                                                            , query.PageSize,
                                                            p => p.IgnoreAutoIncludes().Include(c => c.PostMedias)
                                                            );
            return _mapper.Map<List<PostDto>>(paginationResponse.Items);
        }
        public async Task<PostDto> GetPostByIdAsync(Guid postId)
        {
            var post = await _postRepository.GetFirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted
                                                                    , p => p.Include(c => c.PostMedias).Include(c => c.Comments).Include(c => c.User));
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            post.Comments = post.Comments.Where(p => p.CommentType == CommentType.Parent.ToString()).ToList();
            return _mapper.Map<PostDto>(post);
        }
        public async Task<List<PostDto>> GetReccomendationPostsAsync(PostSearchQueryDto query)
        {
            var userId = _claimService.GetUserId();
            if (!string.IsNullOrEmpty(query.SearchText))
            {
                if (query.IsTag)
                {
                    var postSearchTag = await _postRepository.GetAllAsync(p => p.PostDetailTags.Any(p => p.Tag.Contains(query.SearchText)) && !p.IsDeleted && p.UserId != userId
                                                , p => p.OrderByDescending(p => p.CreatedOn)
                                                , query.PageIndex
                                                , query.PageSize
                                                , p => p.Include(p => p.PostDetailTags).Include(p => p.User).Include(p => p.PostMedias));

                    return _mapper.Map<List<PostDto>>(postSearchTag.Items);
                }
                var postSearchDescription = await _postRepository.GetAllAsync(p => p.Description.Contains(query.SearchText) && !p.IsDeleted && p.UserId != userId
                                                , p => p.OrderByDescending(p => p.CreatedOn)
                                                , query.PageIndex
                                                , query.PageSize
                                                , p => p.Include(p => p.PostDetailTags).Include(p => p.User).Include(p => p.PostMedias));
                return _mapper.Map<List<PostDto>>(postSearchDescription.Items);
            }

            var posts = await _postRepository.GetAllAsync(p => p.UserId != userId && !p.IsDeleted, p => p.OrderByDescending(p => p.CreatedOn)
                                                , query.PageIndex
                                                , query.PageSize
                                                , p => p.Include(p => p.PostDetailTags).Include(p => p.User).Include(p => p.PostMedias));
            return _mapper.Map<List<PostDto>>(posts.Items);
        }
        public async Task<List<CommentDto>> GetAllReplyCommentsAsync(Guid postId, Guid commentId)
        {
            var commentParent = _commentRepository.GetFirstOrDefaultAsync(p => p.Id == commentId && p.CommentType == CommentType.Parent.ToString());
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
            post.Id = Guid.NewGuid();
            var requestPostMedia = await _cloudinaryService.PostMediaToCloudAsync(requestDto.Files, post.Id);
            var postMedias = _mapper.Map<List<PostMedia>>(requestPostMedia);
            await _postRepository.AddAsync(post);
            foreach (var postMedia in postMedias)
            {
                postMedia.PostId = post.Id;
            }
            await _postMediaRepository.AddRangeAsync(postMedias);
            return _mapper.Map<PostDto>(post);
        }
        public async Task<CommentDto> CreateNewCommentAsync(Guid postId, CommentRequestDto requestDto)
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
            comment.CommentType = CommentType.Parent.ToString();
            ++post.CommentCount;
            await _commentRepository.AddAsync(comment);
            await _postRepository.UpdateAsync(post);
            await _userInteractionRepository.AddAsync(userInteraction);
            return _mapper.Map<CommentDto>(comment);
        }
        public async Task<CommentDto> CreateReplyCommentAsync(Guid postId, Guid parentCommentId, CommentRequestDto requestDto)
        {
            if (!(await _commentRepository.AnyAsync(p => p.Id == parentCommentId && p.CommentType == CommentType.Parent.ToString())))
                throw new NotFoundException(ValidationTexts.NotFound.Format("Comment", parentCommentId));
            var userId = _claimService.GetUserId();
            var comment = _mapper.Map<Comment>(requestDto);
            comment.CommentType = CommentType.Reply.ToString();
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
        public async Task<PostDto> UpdatePostByIdAsync(UpdatePostRequestDto postDto, Guid postId)
        {
            Post post = await _postRepository.GetFirstAsync(p => p.Id == postId);
            if (post == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Post", postId));
            if (post.UserId != _claimService.GetUserId())
                throw new ForbiddenException(ValidationTexts.Forbidden.Format("Post", postId));
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
        public async Task<CommentDto> UpdateCommentAsync(CommentRequestDto commentDto, Guid commentId)
        {
            var comment = await _commentRepository.GetFirstOrDefaultAsync(p => p.Id == commentId);
            if (comment == null)
                throw new NotFoundException(ValidationTexts.NotFound.Format("Comment", commentId));
            if (comment.UserPostedId != _claimService.GetUserId())
                throw new ForbiddenException(ValidationTexts.Forbidden.Format("Comment", commentId));
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
            if (post.UserId != _claimService.GetUserId())
                throw new ForbiddenException(ValidationTexts.Forbidden.Format("Post", postId));
            var postMedia = await _postMediaRepository.GetAllAsync(p => p.PostId == postId);
            if (postMedia != null)
            {
                foreach (var media in postMedia)
                {
                    await _postMediaRepository.DeleteAsync(media);
                    await _cloudinaryService.DeleteMediaFromCloudAsync(postId, media.Id);
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
            if (comment.UserPostedId != _claimService.GetUserId())
                throw new ForbiddenException(ValidationTexts.Forbidden.Format("Comment", commentId));
            await _commentRepository.DeleteAsync(comment);
            return _mapper.Map<CommentDto>(comment);
        }
        #endregion
    }
}


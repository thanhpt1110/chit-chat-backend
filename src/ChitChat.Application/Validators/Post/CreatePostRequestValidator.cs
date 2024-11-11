using ChitChat.Application.Models.Dtos.Post.CreatePost;

using FluentValidation;

namespace ChitChat.Application.Validators.Post
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequestDto>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 100 characters");
            RuleFor(x => x.PostMedias)
                .NotEmpty()
                .WithMessage("Post media is required");
        }
    }
}

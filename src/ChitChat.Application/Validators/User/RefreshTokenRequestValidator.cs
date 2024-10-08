using ChitChat.Application.Models.Dtos.User;
using FluentValidation;

namespace ChitChat.Application.Validators.User
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(token => token.RefreshToken).NotEmpty().NotNull();
            RuleFor(token => token.AccessToken).NotEmpty().NotNull();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChitChat.Application.Models.Dtos.User;
using ChitChat.Domain.Identity;
using FluentValidation;
namespace ChitChat.Application.Validators.User
{
    public class RegisterationRequestValidator : AbstractValidator<RegisterationRequestDto>
    {
        public RegisterationRequestValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .Length(1, 255).WithMessage("First name must be between 1 and 255 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(1, 255).WithMessage("Last name must be between 1 and 255 characters.");

            /*           RuleFor(user => user.AvatarUrl)
                           .MaximumLength(255).WithMessage("Avatar URL must be up to 255 characters.");

                       RuleFor(user => user.Bio)
                           .MaximumLength(500).WithMessage("Bio must be up to 500 characters.");

                       RuleFor(user => user.DateOfBirth)
                           .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");

                       RuleFor(user => user.Gender)
                           .Matches("^(Male|Female|Other)$").WithMessage("Gender must be Male, Female, or Other.");

                       RuleFor(user => user.UserStatus)
                           .IsInEnum().WithMessage("User status must be a valid value. User Status must is 0 {Private} and 1 {Public}");
                       */
            RuleFor(user => user.Email)
                .EmailAddress().WithMessage("Email is not valid");
        }
    }

}

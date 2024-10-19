using ChitChat.Application.Models.Dtos.Message;
using FluentValidation;

namespace ChitChat.Application.Validators.Message
{
    public class RequestMessageValidator : AbstractValidator<RequestSendMessageDto>
    {
        public RequestMessageValidator()
        {
            RuleFor(message => message.MessageText).NotEmpty().NotNull();
            RuleFor(message => message.ConversationId).NotEmpty().NotNull();
        }
    }
}

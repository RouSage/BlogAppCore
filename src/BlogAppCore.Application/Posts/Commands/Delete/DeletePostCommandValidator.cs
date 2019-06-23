using FluentValidation;

namespace BlogAppCore.Application.Posts.Commands.Delete
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(i => i.Id).GreaterThan(0);
        }
    }
}
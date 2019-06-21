using BlogAppCore.Domain.Entities;
using FluentValidation;

namespace BlogAppCore.Application.Tags.Commands.Create
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(n => n.Name).NotEmpty().MaximumLength(Tag.MAX_LENGTH);
        }
    }
}
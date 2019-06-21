using BlogAppCore.Domain.Entities;
using FluentValidation;

namespace BlogAppCore.Application.Tags.Commands.Update
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        public UpdateTagCommandValidator()
        {
            RuleFor(i => i.Id).GreaterThan(0);
            RuleFor(n => n.Name).NotEmpty().MaximumLength(Tag.MAX_LENGTH);
        }
    }
}
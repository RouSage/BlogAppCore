using FluentValidation;

namespace BlogAppCore.Application.Tags.Commands.Delete
{
    public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(i => i.Id).GreaterThan(0);
        }
    }
}
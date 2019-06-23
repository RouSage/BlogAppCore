using FluentValidation;

namespace BlogAppCore.Application.Posts.Commands.Update
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(i => i.Id).GreaterThan(0);
            RuleFor(t => t.Title).NotEmpty().MaximumLength(500);
            RuleFor(d => d.Description).NotEmpty().MaximumLength(5000);
            RuleFor(c => c.Content).NotEmpty();
            RuleFor(ci => ci.CategoryId).GreaterThan(0);
        }
    }
}
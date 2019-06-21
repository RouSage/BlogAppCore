using FluentValidation;

namespace BlogAppCore.Application.Posts.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(t => t.Title).NotEmpty().MaximumLength(500);
            RuleFor(d => d.Description).NotEmpty().MaximumLength(5000);
            RuleFor(c => c.Content).NotEmpty();
            RuleFor(ci => ci.CategoryId).GreaterThan(0);
        }
    }
}
using FluentValidation;

namespace BlogAppCore.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(i => i.Id).GreaterThan(0);
        }
    }
}
using BlogAppCore.Domain.Entities;
using FluentValidation;

namespace BlogAppCore.Application.Categories.Commands.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(n => n.Name).NotEmpty().MaximumLength(Category.MAX_LENGTH);
        }
    }
}
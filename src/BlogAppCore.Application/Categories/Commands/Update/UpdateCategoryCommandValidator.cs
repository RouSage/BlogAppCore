using BlogAppCore.Domain.Entities;
using FluentValidation;

namespace BlogAppCore.Application.Categories.Commands.Update
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(i => i.Id).GreaterThan(0);
            RuleFor(n => n.Name).NotEmpty().MaximumLength(Category.MAX_LENGTH);
        }
    }
}
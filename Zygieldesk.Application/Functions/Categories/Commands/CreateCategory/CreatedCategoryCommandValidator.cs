using FluentValidation;

namespace Zygieldesk.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreatedCategoryCommandValidator : AbstractValidator<CreatedCategoryCommand>
    {
        public CreatedCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(15)
                .WithMessage("{PropertyName} Length is between 2 and 15");

            RuleFor(c => c.Description)
                .MaximumLength(300)
                .WithMessage("{PropertyName} Maximum length is 300 characters");
        }
    }
}
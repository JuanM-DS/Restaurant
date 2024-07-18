using FluentValidation;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Shared.Validations
{
    public class DishCategoryValidations : AbstractValidator<DishCategory>
    {
        public DishCategoryValidations()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}

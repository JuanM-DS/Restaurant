using FluentValidation;
using Restaurant.Core.Application.DTOs.Entities;

namespace Restaurant.Infrastructure.Shared.Validations
{
    public class DishValidations : AbstractValidator<DishDto>
    {
        public DishValidations()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Portions)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }
    }
}

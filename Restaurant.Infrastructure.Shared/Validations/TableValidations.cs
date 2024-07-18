using FluentValidation;
using Restaurant.Core.Application.DTOs.Entities;

namespace Restaurant.Infrastructure.Shared.Validations
{
    public class TableValidations : AbstractValidator<TableDto>
    {
        public TableValidations()
        {
            RuleFor(x => x.Guests)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.StatusId)
                .GreaterThan(0);
        }
    }
}

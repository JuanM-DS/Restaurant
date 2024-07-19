using FluentValidation;
using Restaurant.Core.Application.DTOs.Entities;

namespace Restaurant.Infrastructure.Shared.Validations
{
    public class OrderValidations : AbstractValidator<OrderDto>
    {
        public OrderValidations()
        {
            RuleFor(x => x.Subtotal)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.TableId)
                .GreaterThan(0);

            RuleFor(x => x.StatusId)
                .GreaterThan(0);
        }
    }
}

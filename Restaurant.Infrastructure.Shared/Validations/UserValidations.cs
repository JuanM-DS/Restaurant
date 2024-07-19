using FluentValidation;
using Restaurant.Core.Application.DTOs.Entities;

namespace Restaurant.Infrastructure.Shared.Validations
{
    public  class UserValidations : AbstractValidator<SaveApplicationUserDto>
    {
        public UserValidations()
        {
            RuleFor(x=>x.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .Equal(x=>x.Password);
        }
    }
}

﻿using FluentValidation;
using Restaurant.Core.Application.DTOs.Entities;

namespace Restaurant.Infrastructure.Shared.Validations
{
    public class RoleValidations : AbstractValidator<ApplicationRoleDto>
    {
        public RoleValidations()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}

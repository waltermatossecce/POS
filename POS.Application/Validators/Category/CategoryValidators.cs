﻿using FluentValidation;
using POS.Application.Dtos.Category.Request;

namespace POS.Application.Validators.Category
{
    public class CategoryValidators : AbstractValidator<CategoryRequestDto>
    {
        public CategoryValidators()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser vacio");
        }
    }
}

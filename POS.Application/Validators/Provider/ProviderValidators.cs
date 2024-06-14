using FluentValidation;
using POS.Application.Dtos.Provider.Request;

namespace POS.Application.Validators.Provider
{
    public class ProviderValidators : AbstractValidator<ProviderRequestDto>
    {
        public ProviderValidators()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo Nombre no puede estar nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede estar vacio");
        }
    }
}

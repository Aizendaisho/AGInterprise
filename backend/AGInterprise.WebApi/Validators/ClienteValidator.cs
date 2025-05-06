using AGInterprise.Domain.Entities.Facturacion;
using FluentValidation;

namespace AGInterprise.WebApi.Validators;

public class ClienteValidator : AbstractValidator<Cliente>
{
    public ClienteValidator()
    {
        RuleFor(c => c.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

        RuleFor(c => c.Correo)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El correo debe ser válido.")
            .MaximumLength(150);

        RuleFor(c => c.RNC)
            .MaximumLength(20)
            .When(c => !string.IsNullOrWhiteSpace(c.RNC));

        RuleFor(c => c.Telefono)
            .MaximumLength(15);

        RuleFor(c => c.Direccion)
            .MaximumLength(200);
    }
}

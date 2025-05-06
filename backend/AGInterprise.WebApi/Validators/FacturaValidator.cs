using AGInterprise.Domain.Entities.Facturacion;
using FluentValidation;

namespace AGInterprise.WebApi.Validators;

public class FacturaValidator : AbstractValidator<Factura>
{
    public FacturaValidator()
    {
        RuleFor(f => f.ClienteId)
            .GreaterThan(0).WithMessage("Debe seleccionar un cliente válido.");

        RuleFor(f => f.FechaFactura)
            .NotEmpty().WithMessage("La fecha de la factura es obligatoria.");

        RuleFor(f => f.Detalles)
            .NotNull().WithMessage("La factura debe tener al menos un detalle.")
            .Must(d => d.Count > 0).WithMessage("Debe agregar al menos un producto a la factura.");
    }
}

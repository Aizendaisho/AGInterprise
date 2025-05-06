using FluentValidation;
using AGInterprise.WebApi.Models.Requests;

public class FacturaRequestValidator : AbstractValidator<FacturaRequest>
{
    public FacturaRequestValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0).WithMessage("Debe indicar un Cliente válido.");

        RuleFor(x => x.FechaFactura)
            .NotEmpty().WithMessage("La fecha de factura es obligatoria.")
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1))
            .WithMessage("La fecha de factura no puede ser futura.");

        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("La factura debe tener al menos un detalle.");

        RuleForEach(x => x.Detalles)
            .SetValidator(new DetalleFacturaRequestValidator());
    }
}

using FluentValidation;
using AGInterprise.WebApi.Models.Requests;

public class DetalleMovimientoRequestValidator : AbstractValidator<DetalleMovimientoRequest>
{
    public DetalleMovimientoRequestValidator()
    {
        RuleFor(x => x.ProductoId)
            .GreaterThan(0).WithMessage("Debe indicar un Producto válido.");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.");
    }
}

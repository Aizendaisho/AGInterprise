using FluentValidation;
using AGInterprise.WebApi.Models.Requests;

public class DetalleFacturaRequestValidator : AbstractValidator<DetalleFacturaRequest>
{
    public DetalleFacturaRequestValidator()
    {
        RuleFor(x => x.ProductoId)
            .GreaterThan(0).WithMessage("Debe indicar un Producto válido.");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero.");

        RuleFor(x => x.PrecioUnitario)
            .GreaterThan(0).WithMessage("El precio unitario debe ser mayor que cero.");
    }
}

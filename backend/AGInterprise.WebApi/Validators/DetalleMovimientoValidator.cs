using AGInterprise.Domain.Entities.Inventario;
using FluentValidation;

namespace AGInterprise.WebApi.Validators;

public class DetalleMovimientoValidator : AbstractValidator<DetalleMovimiento>
{
    public DetalleMovimientoValidator()
    {
        RuleFor(d => d.ProductoId)
            .GreaterThan(0).WithMessage("Debe seleccionar un producto válido.");

        RuleFor(d => d.Cantidad)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
    }
}

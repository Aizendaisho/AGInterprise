using AGInterprise.Domain.Entities.Inventario;
using FluentValidation;

namespace AGInterprise.WebApi.Validators;

public class MovimientoInventarioValidator : AbstractValidator<MovimientoInventario>
{
    public MovimientoInventarioValidator()
    {
        RuleFor(m => m.Tipo)
            .NotEmpty().WithMessage("El tipo de movimiento es obligatorio.")
            .Must(t => new[] { "Entrada", "Salida", "Transferencia" }
                        .Contains(t))
            .WithMessage("Tipo inválido. Debe ser Entrada, Salida o Transferencia.");

        // Si es Salida o Transferencia, debe venir AlmacenOrigenId
        RuleFor(m => m.AlmacenOrigenId)
            .NotNull()
            .When(m => m.Tipo == "Salida" || m.Tipo == "Transferencia")
            .WithMessage("Debe especificar el almacén de origen para Salida o Transferencia.");

        // Si es Entrada o Transferencia, debe venir AlmacenDestinoId
        RuleFor(m => m.AlmacenDestinoId)
            .NotNull()
            .When(m => m.Tipo == "Entrada" || m.Tipo == "Transferencia")
            .WithMessage("Debe especificar el almacén de destino para Entrada o Transferencia.");

        RuleFor(m => m.Detalles)
            .NotNull().WithMessage("El movimiento debe tener al menos un detalle.")
            .Must(d => d.Count > 0).WithMessage("Debe agregar al menos un detalle al movimiento.");
    }
}

using FluentValidation;
using AGInterprise.WebApi.Models.Requests;

public class MovimientoInventarioRequestValidator : AbstractValidator<MovimientoInventarioRequest>
{
    public MovimientoInventarioRequestValidator()
    {
        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("Debe indicar el tipo de movimiento.")
            .Must(t => t == "Entrada" || t == "Salida" || t == "Transferencia")
            .WithMessage("Tipo inválido. Debe ser Entrada, Salida o Transferencia.");

        RuleFor(x => x.Comentario)
            .MaximumLength(200).WithMessage("El comentario no puede exceder 200 caracteres.");

        RuleFor(x => x.Detalles)
            .NotEmpty().WithMessage("Debe existir al menos un detalle de movimiento.");

        RuleForEach(x => x.Detalles)
            .SetValidator(new DetalleMovimientoRequestValidator());

        When(x => x.Tipo == "Entrada", () =>
        {
            RuleFor(x => x.AlmacenDestinoId)
                .GreaterThan(0)
                .WithMessage("Para Entrada debe indicarse almacenDestinoId.");
        });

        When(x => x.Tipo == "Salida" || x.Tipo == "Transferencia", () =>
        {
            RuleFor(x => x.AlmacenOrigenId)
                .GreaterThan(0)
                .WithMessage("Para Salida/Transferencia debe indicarse almacenOrigenId.");
        });

        When(x => x.Tipo == "Transferencia", () =>
        {
            RuleFor(x => x.AlmacenDestinoId)
                .GreaterThan(0)
                .WithMessage("Para Transferencia debe indicarse almacenDestinoId.");
        });
    }
}

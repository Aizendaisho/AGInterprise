using AGInterprise.Domain.Entities.Inventario;
using FluentValidation;

namespace AGInterprise.WebApi.Validators;

public class ProductoValidator : AbstractValidator<Producto>
{
    public ProductoValidator()
    {
        RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
            .MaximumLength(100);

        RuleFor(p => p.Categoria)
            .MaximumLength(100);

        RuleFor(p => p.UnidadMedida)
            .MaximumLength(50);

        RuleFor(p => p.PrecioUnitario)
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0.");
    }
}

using Xunit;
using FluentValidation.TestHelper;
using AGInterprise.WebApi.Models.Requests;
using AGInterprise.WebApi.Validators;

public class FacturaRequestValidatorTests
{
    private readonly FacturaRequestValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_ClienteIdIsZero()
    {
        var model = new FacturaRequest { ClienteId = 0, FechaFactura = DateTime.UtcNow, Detalles = { new() { ProductoId=1, Cantidad=1, PrecioUnitario=10 } } };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ClienteId);
    }

    [Fact]
    public void Should_HaveError_When_DetallesEmpty()
    {
        var model = new FacturaRequest { ClienteId = 1, FechaFactura = DateTime.UtcNow };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Detalles);
    }

    [Fact]
    public void Should_NotHaveErrors_When_ValidModel()
    {
        var model = new FacturaRequest
        {
            ClienteId    = 1,
            FechaFactura = DateTime.UtcNow,
            Detalles     = { new() { ProductoId=1, Cantidad=2, PrecioUnitario=15 } }
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}

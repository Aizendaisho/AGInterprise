using AGInterprise.Domain.Entities.Facturacion;

namespace AGInterprise.Application.Interfaces;

public interface IFacturaService
{
Task<Factura> CrearFacturaCompletaAsync(Factura factura, int usuarioId);
}

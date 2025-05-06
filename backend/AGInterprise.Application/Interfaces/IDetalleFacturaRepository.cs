using AGInterprise.Domain.Entities.Facturacion;

namespace AGInterprise.Application.Interfaces;

public interface IDetalleFacturaRepository
{
    Task<IEnumerable<DetalleFactura>> ObtenerTodosAsync();
    Task<DetalleFactura?> ObtenerPorIdAsync(int id);
    Task CrearAsync(DetalleFactura detalle);
    Task ActualizarAsync(DetalleFactura detalle);
    Task EliminarAsync(int id);
}

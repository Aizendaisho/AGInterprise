using AGInterprise.Domain.Entities.Inventario;

namespace AGInterprise.Application.Interfaces;

public interface IDetalleMovimientoRepository
{
    Task<IEnumerable<DetalleMovimiento>> ObtenerTodosAsync();
    Task<DetalleMovimiento?> ObtenerPorIdAsync(int id);
    Task CrearAsync(DetalleMovimiento detalle);
    Task ActualizarAsync(DetalleMovimiento detalle);
    Task EliminarAsync(int id);
}
